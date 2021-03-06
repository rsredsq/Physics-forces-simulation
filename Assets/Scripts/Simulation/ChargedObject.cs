﻿using UnityEngine;
using UnityEngine.Events;

namespace Simulation {
  [RequireComponent(typeof(Rigidbody))]
  public class ChargedObject : MonoBehaviour {
    public ChargedObjectData Data {
      get { return new ChargedObjectData(transform.position, charge, startVelocity, Rigidbody.mass); }
      set {
        charge = value.Charge;
        startVelocity = value.StartVelocity;
        Rigidbody.mass = value.Mass;
        transform.position = value.Position;
      }
    }

    [SerializeField]
    private float charge;

    public UnityEvent OnChargeChange;

    public float Charge {
      get { return charge; }
      set {
        SimulationSystem.PendUpdate(() => {
          charge = value;
          OnChargeChange.Invoke();
        });
      }
    }

    [SerializeField]
    public Vector3 startVelocity;

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
      OnChargeChange.Invoke();
    }

    private void OnCollisionEnter(Collision other) {
      var otherChargedObject = other.gameObject.GetComponent<ChargedObject>();

      if (otherChargedObject != null) {
        UpdateCharge(otherChargedObject);
      }
    }

    private void UpdateCharge(ChargedObject otherChargeObject) {
      var newCharge = (Charge + otherChargeObject.Charge) / 2;
      Charge = newCharge;
      otherChargeObject.Charge = newCharge;
    }
  }
}
