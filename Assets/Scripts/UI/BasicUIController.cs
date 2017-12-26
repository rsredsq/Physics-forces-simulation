using Simulation;
using UnityEngine;
using UnityEngine.Networking;
using Utils;

namespace UI {
  public class BasicUIController : MonoBehaviour {
    private Camera followingCamera;

    public Texture2D crosshairImage;

    private void Awake() {
      PickCamera();
    }

    private void PickCamera() {
      followingCamera = GameObject.FindGameObjectWithTag("FollowingCamera").GetComponent<Camera>();
    }


    private void OnGUI() {
      GUI.DrawTexture(new Rect(Screen.width / 2 - 16, Screen.height / 2 - 16, 32, 32), crosshairImage);

      if (AppManager.Instance.EditorModeEnabled) {
        GUILayout.Label("[Editor Mode]");
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
      GUILayout.BeginVertical("", GUI.skin.box);
      GUILayout.Label(string.Format("Заряд: {0:0.00} Кл", chargedObject.Charge));
      GUILayout.Label(string.Format("Скорость: {0} мм/с", chargedObject.Rigidbody.velocity.ToString("0.00")));
      GUILayout.Label(string.Format("Сила Лоренца: {0} Н", chargedObject.LorentzForce.ToString("0.00")));
      GUILayout.Label(string.Format("Сила Кулона: {0} Н", chargedObject.CoulombForce.ToString("0.00")));
      GUILayout.EndVertical();
    }
  }
}
