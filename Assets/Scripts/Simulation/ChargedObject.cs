using UnityEngine;

namespace Simulation {
  [RequireComponent(typeof(Rigidbody))]
  public class ChargedObject : MonoBehaviour {
    [SerializeField]
    private float charge;

    public float Charge {
      get { return charge; }
      set { SimulationSystem.PendUpdate(() => { charge = value; }); }
    }

    [SerializeField]
    private Vector3 startVelocity;

    [HideInInspector]
    public Vector3 CoulombForce = Vector3.zero;
    [HideInInspector]
    public Vector3 LorentzForce = Vector3.zero;

    public Rigidbody Rigidbody {
      get { return GetComponent<Rigidbody>(); }
    }

    public SimulationSystem SimulationSystem {
      get { return GetComponentInParent<SimulationSystem>(); }
    }

    private void Awake() {
      Rigidbody.useGravity = false;
    }

    private void Start() {
      Rigidbody.velocity = startVelocity;
    }

    private void OnCollisionEnter(Collision other) {
      var otherChargedObject = other.gameObject.GetComponent<ChargedObject>();

      if (otherChargedObject != null) {
        updateCharge(otherChargedObject);
      }
    }

    private void updateCharge(ChargedObject otherChargeObject) {
      var newCharge = (Charge + otherChargeObject.Charge) / 2;
      Charge = newCharge;
      otherChargeObject.Charge = newCharge;
    }
  }
}
