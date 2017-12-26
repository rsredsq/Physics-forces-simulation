using Simulation;
using UI;
using UnityEngine;

namespace Utils {
  public class CameraSwitchController : MonoBehaviour {
    private Camera mainCamera;
    private Camera followingCamera;
    public EditorController Controller;

    private void Awake() {
      PickCameras();
    }

    private void PickCameras() {
      mainCamera = Camera.main;
      followingCamera = GameObject.FindGameObjectWithTag("FollowingCamera").GetComponent<Camera>();
    }

    private void Update() {
      if (mainCamera.enabled) {
        var forward = mainCamera.transform.TransformDirection(Vector3.forward);
        RaycastHit raycastHit;
        if (!Physics.Raycast(mainCamera.transform.position, forward, out raycastHit)) return;
        var hitObject = raycastHit.transform.gameObject;
        var chargedObject = hitObject.GetComponent<ChargedObject>();
        if (chargedObject == null || !Input.GetKeyDown(KeyCode.Space) || Controller.EditorMode) return;
        EnableSecondaryCamera();
        followingCamera.GetComponent<FollowCam>().Target = chargedObject.transform;
      } else if (followingCamera.enabled) {
        if (Input.GetKeyDown(KeyCode.Space)) {
          EnableMainCamera();
        }
      }
    }

    private void EnableMainCamera() {
      mainCamera.enabled = true;
      followingCamera.enabled = false;
    }

    private void EnableSecondaryCamera() {
      mainCamera.enabled = false;
      followingCamera.enabled = true;
    }
  }
}
