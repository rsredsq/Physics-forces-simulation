using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Simulation.Actions;
using UnityEngine;

namespace Simulation {
  public class SimulationSystem : MonoBehaviour {
    private readonly List<ChargedObject> physicsObjects = new List<ChargedObject>();

    private readonly Action<ChargedObject, ChargedObject> calculateForces = FormulasAggregator.ApplyForces;


    void Start() {
      GetComponentsInChildren(true, physicsObjects);
    }

    void Update() {
    }

    void FixedUpdate() {
      physicsObjects.ForEach(self => {
        physicsObjects
          .Where(me => !ReferenceEquals(me, self))
          .ToList()
          .ForEach(other => { calculateForces(self, other); });
      });
    }

    public void PendUpdate(Action setToPending) {
      StartCoroutine(PendingCouroutine(setToPending));
    }

    private IEnumerator PendingCouroutine(Action setToPending) {
      setToPending();
      yield return new WaitForFixedUpdate();
    }
  }
}
