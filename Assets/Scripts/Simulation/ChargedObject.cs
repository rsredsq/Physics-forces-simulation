using NUnit.Framework.Constraints;
using UnityEngine;

namespace Simulation {
  public class ChargedObject : MonoBehaviour {
    [SerializeField]
    private float charge;

    public float Charge {
      get { return charge; }
      set { SimulationSystem.PendUpdate(() => { charge = value; }); }
    }

    [SerializeField]
    private Vector3 velocity;

    public Vector3 Velocity {
      get { return velocity; }
      set { SimulationSystem.PendUpdate(() => { velocity = value; }); }
    }

    public Vector3 CoulombForce = Vector3.zero;
    public Vector3 LorentzForce = Vector3.zero;


    public Rigidbody Rigidbody {
      get {
        if (!GetComponent<Rigidbody>()) {
          gameObject.AddComponent<Rigidbody>();
        }
        return GetComponent<Rigidbody>();
      }
    }

    public SimulationSystem SimulationSystem {
      get { return GetComponentInParent<SimulationSystem>(); }
    }

    private void Awake() {
      Rigidbody.useGravity = false;
    }

    private void Start() {
      Rigidbody.velocity = Velocity;
    }

    private void OnCollisionEnter(Collision other) {
      var otherGameObject = other.gameObject;

      if (otherGameObject.name != "Positive" && otherGameObject.name != "Negative") {
        return;
      }

      var otherChargeObject = other.gameObject.GetComponent<ChargedObject>();

      var newCharge = (this.Charge + otherChargeObject.Charge) / 2;

      this.Charge = newCharge;
      otherChargeObject.Charge = newCharge;
    }
  }
}
