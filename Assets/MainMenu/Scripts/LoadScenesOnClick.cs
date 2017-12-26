using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesOnClick : MonoBehaviour {

  public static void LoadByIndex(int sceneIndex) {
    SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
  }

  public void LoadByIndexObj(int sceneIndex) {
    LoadByIndex(sceneIndex);
  }
}
