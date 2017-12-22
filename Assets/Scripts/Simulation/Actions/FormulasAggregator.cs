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

    static bool isValidForce(Vector3 force) {
      return !float.IsNaN(force.x) && !float.IsNaN(force.y) && !float.IsNaN(force.z);
    }

    private static void CalcCoulombForce(ChargedObject self, ChargedObject other) {
      var distance = Vector3.Distance(self.transform.position, other.transform.position);
      var force = PhysicsConstants.COULOMB_KOEF * self.Charge * other.Charge / Mathf.Pow(distance, 2f);

      var direction = self.transform.position - other.transform.position;
      direction.Normalize();

      var forceWithDirection = force * direction;

      forceWithDirection.x = (float) Math.Round(forceWithDirection.x, 0);
      forceWithDirection.y = (float) Math.Round(forceWithDirection.y, 0);
      forceWithDirection.z = (float) Math.Round(forceWithDirection.z, 0);

      if (Math.Abs(Vector3.SqrMagnitude(forceWithDirection)) <= PhysicsConstants.ACCURACY) return;
      if (!isValidForce(forceWithDirection)) return;

      self.Rigidbody.AddForce(forceWithDirection * Time.fixedDeltaTime);
      self.CoulombForce += forceWithDirection;
    }

    private static void CalcLorentzForce(ChargedObject self, ChargedObject other) {
      var distance = Vector3.Distance(self.transform.position, other.transform.position);

      var direction = self.transform.position - other.transform.position;
      direction.Normalize();

      var magneticInduction = Vector3.Cross(other.Rigidbody.velocity, direction) *
                              (PhysicsConstants.LORENTZ_FOEF * other.Charge / Mathf.Pow(distance, 2f));

      var force = self.Charge * Vector3.Cross(self.Rigidbody.velocity, magneticInduction);

      if (Math.Abs(Vector3.SqrMagnitude(force)) <= PhysicsConstants.ACCURACY) return;
      if (!isValidForce(force)) return;

      self.Rigidbody.AddForce(force * Time.fixedDeltaTime);
      self.LorentzForce += force;
    }
  }
}
