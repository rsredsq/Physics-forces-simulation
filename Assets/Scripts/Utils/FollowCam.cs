using Simulation;
using UnityEngine;

namespace Utils {
  [RequireComponent(typeof(ChargedObject))]
  public class FollowCam : MonoBehaviour {
    private Transform target;

    public Transform Target {
      get { return target; }
      set {
        target = value;
        localPosition = target.InverseTransformPoint(Position);
        maxDistance = Vector3.Distance(Position, target.position);
      }
    }

    public float SpeedX = 360f;
    public float SpeedY = 240f;
    public float LimitY = 70f;

    public LayerMask Obstances;

    private float maxDistance;

    private Vector3 localPosition;
    private float currentYRotation;

    private Vector3 Position {
      get { return transform.position; }
      set { transform.position = value; }
    }

    private void LateUpdate() {
      if (!IsCusorLocked() || Target == null)
        return;

      Position = Target.TransformPoint(localPosition);
      CameraRotation();
      ObstaclesReact();
      localPosition = Target.InverseTransformPoint(Position);
    }

    private void CameraRotation() {
      RotationMouseY();
      RotationMouseX();

      transform.LookAt(Target);
    }

    private void RotationMouseX() {
      var nx = Input.GetAxis("Mouse X");

      if (Mathf.Approximately(nx, 0f)) {
        return;
      }

      transform.RotateAround(Target.position, Vector3.up, nx * SpeedX * Time.deltaTime);
    }

    private void RotationMouseY() {
      var ny = Input.GetAxis("Mouse Y");

      if (Mathf.Approximately(ny, 0f)) {
        return;
      }

      var tmp = Mathf.Clamp(currentYRotation + ny * SpeedY * Time.deltaTime, -LimitY, LimitY);

      if (Mathf.Approximately(tmp, currentYRotation)) {
        return;
      }

      var rot = tmp - currentYRotation;
      transform.RotateAround(Target.position, transform.right, rot);
      currentYRotation = tmp;
    }

    private void ObstaclesReact() {
      var distance = Vector3.Distance(Position, Target.position);
      RaycastHit hit;

      if (Physics.Raycast(Target.position, transform.position - Target.position, out hit, maxDistance, Obstances)) {
        Position = hit.point;
      } else if (distance < maxDistance && !Physics.Raycast(Position, -transform.forward, .1f, Obstances)) {
        Position -= transform.forward * .05f;
      }
    }

    private static bool IsCusorLocked() {
      return Cursor.lockState != CursorLockMode.None;
    }
  }
}
