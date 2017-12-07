using UnityEngine;
using Utils;

namespace Simulation {
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

    private void LateUpdate() {
      UpdateColor();
    }

    private void UpdateColor() {
      if (Charge < 0) {
        render.material.color = ChargeColors.red;
      } else if (Charge > 0) {
        render.material.color = ChargeColors.green;
      } else {
        render.material.color = ChargeColors.grey;
      }
    }
  }
}
