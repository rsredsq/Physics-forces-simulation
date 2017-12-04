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
    private float someTestForce;

    public float SomeTestForce {
      get { return someTestForce; }
      set { SimulationSystem.PendUpdate(() => { someTestForce = value; }); }
    }

    public Vector3 StartVelocity;

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
      Rigidbody.velocity = StartVelocity;
    }
  }
}
