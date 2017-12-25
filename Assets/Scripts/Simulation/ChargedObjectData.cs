using System;
using UnityEngine;

namespace Simulation {
  [Serializable]
  public class ChargedObjectData {

    public ChargedObjectData(float charge, Vector3 startVelocity, float mass) {
      this.Charge = charge;
      this.StartVelocity = startVelocity;
      this.Mass = mass;
    }

    public float Charge;
    public Vector3 StartVelocity;
    public float Mass;
  }
}
