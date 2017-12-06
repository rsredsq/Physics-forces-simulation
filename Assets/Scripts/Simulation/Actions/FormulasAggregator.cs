using System.Collections.Generic;
using UnityEngine;

namespace Simulation.Actions {
  internal static class FormulasAggregator {
    public static void ApplyForces(ChargedObject self, List<ChargedObject> others) {
      SimpleChargeMagnetic(self, others);
    }

    private static void SimpleChargeMagnetic(ChargedObject self, List<ChargedObject> others) {
      others.ForEach((other) => {
        float distance = Vector3.Distance(self.transform.position, other.transform.position);
        float force = (float)(PhysicsConstants.COULOMB_KOEF * self.Charge * other.Charge / Mathf.Pow(distance, 2));

        var direction = self.transform.position - other.transform.position;
        direction.Normalize();

        var newForce = force * direction * Time.fixedDeltaTime;

        self.Rigidbody.AddForce(newForce);
      });
    }
  }
}
