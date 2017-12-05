using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Simulation.Actions;
using UnityEngine;

namespace Simulation{
  public class SimulationSystem : MonoBehaviour{
    private readonly List<ChargedObject> physicsObjects = new List<ChargedObject>();

    private readonly Action<ChargedObject, List<ChargedObject>> calculateForces = FormulasAggregator.ApplyForces;


    void Start(){
      GetComponentsInChildren(true, physicsObjects);
    }

    void FixedUpdate(){
      physicsObjects.ForEach(self => {
        var others = physicsObjects
          .Where(me => !ReferenceEquals(me, self))
          .ToList();
        calculateForces(self, others);
      });
    }

    public void PendUpdate(Action setToPending){
      StartCoroutine(PendingCouroutine(setToPending));
    }

    private IEnumerator PendingCouroutine(Action setToPending){
      yield return new WaitForFixedUpdate();
      setToPending();
    }
  }
}
