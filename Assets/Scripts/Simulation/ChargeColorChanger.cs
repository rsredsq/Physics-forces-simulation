using UnityEngine;

namespace Simulation {
  [ExecuteInEditMode]
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
        render.sharedMaterial.color = Color.red;
      } else if (Charge > 0) {
        render.sharedMaterial.color = Color.green;
      } else {
        render.sharedMaterial.color = Color.grey;
      }
    }
  }
}
