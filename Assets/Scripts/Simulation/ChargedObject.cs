using UnityEngine;

namespace Simulation{
  public class ChargedObject : MonoBehaviour{
    [SerializeField] private float charge;

    public float Charge{
      get{ return charge; }
      set{ SimulationSystem.PendUpdate(() => { charge = value; }); }
    }

    [SerializeField] private Vector3 velocity;

    public Vector3 Velocity{
      get{ return velocity; }
      set{ SimulationSystem.PendUpdate(() => { velocity = value; }); }
    }

    public Vector3 CoulombForce;
    public Vector3 LorentzForce;
    public Vector3 ElectromagneticForce;


    public Rigidbody Rigidbody{
      get{
        if (!GetComponent<Rigidbody>()){
          gameObject.AddComponent<Rigidbody>();
        }
        return GetComponent<Rigidbody>();
      }
    }

    public SimulationSystem SimulationSystem{
      get{ return GetComponentInParent<SimulationSystem>(); }
    }

    private void Awake(){
      Rigidbody.useGravity = false;
    }

    private void Start(){
      Rigidbody.velocity = Velocity;
    }

    private void OnMouseDown(){
      Rigidbody.AddForce(transform.forward * 100);
    }
  }
}
