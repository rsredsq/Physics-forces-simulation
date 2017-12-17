using UnityEngine;

namespace UI {
  public class BasicUiController : MonoBehaviour {
    private void OnGUI() {
      if (AppManager.Instance.EditorModeEnabled) {
        GUILayout.Label("[Editor Mode]");
      }
    }
  }
}
