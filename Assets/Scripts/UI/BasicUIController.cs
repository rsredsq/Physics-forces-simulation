using System;
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
        GUILayout.Label("[Editor Mode]");
      }
      if (followingCamera.enabled) {
        ShowPhysicsConsts();
      }
    }

    private void ShowPhysicsConsts() {
      var target = followingCamera.GetComponent<FollowCam>().Target.gameObject.GetComponent<ChargedObject>();
      if (target == null) return;
      GUILayout.Label(string.Format("Charge: {0}", target.Charge));
    }
  }
}
