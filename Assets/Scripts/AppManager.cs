using UnityEngine;

public class AppManager : MonoBehaviour {
  private static AppManager instance;

  public static AppManager Instance {
    get { return instance ?? (instance = FindObjectOfType<AppManager>()); }
  }

  private bool editorModeEnabled;

  public bool EditorModeEnabled {
    get { return editorModeEnabled; }
    set {
      editorModeEnabled = value;
    }
  }

  private void OnGUI() {
    GUILayout.Label("Test");
  }
}
