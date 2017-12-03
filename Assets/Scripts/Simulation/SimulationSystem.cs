using System;
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
  }
}
