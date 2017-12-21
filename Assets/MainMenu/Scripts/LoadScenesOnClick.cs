using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesOnClick : MonoBehaviour {

  public void LoadByIndex(int sceneIndex) {
    SceneManager.LoadScene(sceneIndex);
  }

}
