using UnityEngine;

namespace Simulation {
  public class ChargedObject : MonoBehaviour {
    public float Charge;
    public Vector3 StartVelocity;

    public Rigidbody Rigidbody {
      get {
        if (!GetComponent<Rigidbody>()) {
          gameObject.AddComponent<Rigidbody>();
        }
        return GetComponent<Rigidbody>();
      }
    }

    private void Awake() {
      Rigidbody.useGravity = false;
    }

    private void Start() {
      Rigidbody.velocity = StartVelocity;
    }
  }
}
