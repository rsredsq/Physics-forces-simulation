using UnityEngine;
using Utils;

namespace Simulation {
  [RequireComponent(typeof(Renderer))]
  [RequireComponent(typeof(ChargedObject))]
  public class ChargeColorChanger : MonoBehaviour {
    private Renderer render;
    private ChargedObject chargedObject;

    private float Charge {
      get { return chargedObject.Charge; }
    }

    void Awake() {
      render = GetComponent<Renderer>();
      chargedObject = GetComponent<ChargedObject>();
    }

    public void OnChargeChange() {
      if (Charge < 0) {
        render.material.color = CustomColors.red;
      } else if (Charge > 0) {
        render.material.color = CustomColors.green;
      } else {
        render.material.color = CustomColors.grey;
      }
    }
  }
}
