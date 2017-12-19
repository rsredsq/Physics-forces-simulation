using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Simulation.Actions;
using UnityEngine;

namespace Simulation {
  public class SimulationSystem : MonoBehaviour {
    private readonly List<ChargedObject> physicsObjects = new List<ChargedObject>();

    private readonly Stack<Dictionary<int, Vector3>> velocitiesStack = new Stack<Dictionary<int, Vector3>>(2);

    private readonly Action<ChargedObject, List<ChargedObject>> calculateForces = FormulasAggregator.ApplyForces;

    void Start() {
      GetComponentsInChildren(true, physicsObjects);
    }

    void FixedUpdate() {
      if (AppManager.Instance.EditorModeEnabled && !simulationPaused) {
        PauseObjectsSimulation();
        return;
      }

      if (simulationPaused) {
        ResumeSimulation();
      }

      PhysicsTick();
    }

    private void ResumeSimulation() {
      if (!simulationPaused) return;
      physicsObjects.ForEach(cObj => {
        var rig = cObj.GetComponent<Rigidbody>();
        rig.WakeUp();
      });

      PopVelocity();
      simulationPaused = false;
    }

    private bool simulationPaused;

    private void PauseObjectsSimulation() {
      if (simulationPaused) return;
      PushVelocity();
      physicsObjects.ForEach(cObj => {
        var rig = cObj.GetComponent<Rigidbody>();
        rig.Sleep();
      });
      simulationPaused = true;
    }

    private void PushVelocity() {
      var velocities = physicsObjects
        .Select(cObj => cObj.GetComponent<Rigidbody>())
        .ToDictionary(rig => rig.GetInstanceID(), rig => rig.velocity);

      velocitiesStack.Push(velocities);
    }

    private void PopVelocity() {
      var velocities = velocitiesStack.Pop();

      physicsObjects
        .Select(cObj => cObj.GetComponent<Rigidbody>())
        .Where(rig => velocities.ContainsKey(rig.GetInstanceID()))
        .ToList()
        .ForEach(rig => rig.velocity = velocities[rig.GetInstanceID()]);
    }

    private void PhysicsTick() {
      physicsObjects.ForEach(self => {
        ResetForces(self);

        var others = physicsObjects
          .Where(me => !ReferenceEquals(me, self))
          .ToList();
        calculateForces(self, others);
      });
    }

    private void ResetForces(ChargedObject self) {
      self.CoulombForce = Vector3.zero;
      self.LorentzForce = Vector3.zero;
    }

    public void PendUpdate(Action setToPending) {
      StartCoroutine(PendingCouroutine(setToPending));
    }

    private IEnumerator PendingCouroutine(Action setToPending) {
      yield return new WaitForFixedUpdate();
      setToPending();
    }

    public void AddNewChargedObject(ChargedObject obj) {
      physicsObjects.Add(obj);
    }
  }
}
