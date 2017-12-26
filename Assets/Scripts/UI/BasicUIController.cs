using Simulation;
using UnityEngine;
using Utils;

namespace UI {
  public class BasicUIController : MonoBehaviour {
    private Camera followingCamera;

    private void Awake() {
      PickCamera();
    }

    private void PickCamera() {
      followingCamera = GameObject.FindGameObjectWithTag("FollowingCamera").GetComponent<Camera>();
    }

    private void OnGUI() {
      if (AppManager.Instance.EditorModeEnabled) {
        GUI.Label(new Rect(0, 0, 200, 50), "[Editor Mode]");
      }
      if (followingCamera.enabled) {
        ShowPhysicsConsts();
      }
    }

    private void ShowPhysicsConsts() {
      var target = followingCamera.GetComponent<FollowCam>().Target;
      if (target == null) return;
      var chargedObject = target.GetComponent<ChargedObject>();
      if (chargedObject == null) return;
      GUI.BeginGroup(new Rect(0, 20, 200, 100));
      GUI.Box(new Rect(0, 0, 200, 100), "");
      GUILayout.Label(string.Format("Заряд: {0} u", chargedObject.Charge));
      GUILayout.Label(string.Format("Скорость: {0} u/ч", chargedObject.Rigidbody.velocity));
      GUILayout.Label(string.Format("Сила Лоренца: {0} Н", chargedObject.LorentzForce));
      GUILayout.Label(string.Format("Сила Кулона: {0} Н", chargedObject.CoulombForce));
      GUI.EndGroup();
    }
  }
}
