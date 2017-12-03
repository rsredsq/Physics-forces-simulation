using UnityEngine;

namespace Simulation.Actions {
  internal static class FormulasAggregator {
    public static void ApplyForces(ChargedObject self, ChargedObject other) {
      SimpleChargeMagnetic(self, other);
    }

    private static void SimpleChargeMagnetic(ChargedObject self, ChargedObject other) {
      float distance = Vector3.Distance(self.transform.position, other.transform.position);
      float force = 1000 * self.Charge * other.Charge / Mathf.Pow(distance, 2);
      Vector3 direction;

      direction = self.transform.position - other.transform.position;
      direction.Normalize();

      var newForce = force * direction * Time.fixedDeltaTime;

      self.Rigidbody.AddForce(newForce);
    }
  }
}
