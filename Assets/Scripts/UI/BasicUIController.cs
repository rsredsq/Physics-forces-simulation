using UnityEngine;

namespace UI {
  public class BasicUIController : MonoBehaviour {
    private void OnGUI() {
      if (AppManager.Instance.EditorModeEnabled) {
        GUILayout.Label("[Editor Mode]");
      }
    }
  }
}
