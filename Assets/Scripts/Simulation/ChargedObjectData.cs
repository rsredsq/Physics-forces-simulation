using System;
using UnityEngine;

namespace Simulation {
  [Serializable]
  public class ChargedObjectData {
    public ChargedObjectData(Vector3 position, float charge, Vector3 startVelocity, float mass) {
      Charge = charge;
      StartVelocity = startVelocity;
      Mass = mass;
      Position = position;
    }

    public float Charge;
    public Vector3 StartVelocity;
    public float Mass;
    public Vector3 Position;
  }
}
