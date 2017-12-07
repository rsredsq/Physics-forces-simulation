using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation.Actions {
  internal static class FormulasAggregator {
    public static void ApplyForces(ChargedObject self, List<ChargedObject> others) {
      others.ForEach((other) => {
        CalcCoulombForce(self, other);
        CalcLorentzForce(self, other);
      });
    }

    private static void CalcCoulombForce(ChargedObject self, ChargedObject other) {
      var distance = Vector3.Distance(self.transform.position, other.transform.position);
      var force = (float) (PhysicsConstants.COULOMB_KOEF * self.Charge * other.Charge / Mathf.Pow(distance, 2));

      var direction = self.transform.position - other.transform.position;
      direction.Normalize();

      var forceWithDirection = force * direction * Time.fixedDeltaTime;

      self.Rigidbody.AddForce(forceWithDirection);
      self.CoulombForce += forceWithDirection / Time.fixedDeltaTime;
    }

    private static void CalcLorentzForce(ChargedObject self, ChargedObject other) {
      var distance = Vector3.Distance(self.transform.position, other.transform.position);

      var direction = self.transform.position - other.transform.position;
      direction.Normalize();

      var magneticInduction = Vector3.Cross(other.Rigidbody.velocity, direction) *
                              (float) (PhysicsConstants.LORENTZ_FOEF * other.Charge / Math.Pow(distance, 2));

      var force = self.Charge * Vector3.Cross(self.Rigidbody.velocity, magneticInduction) * Time.fixedDeltaTime;

      self.Rigidbody.AddForce(force);
      self.LorentzForce += force / Time.fixedDeltaTime;
    }
  }
}
