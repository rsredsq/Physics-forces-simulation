using UnityEngine;

namespace Utils {
  public class FlyCam : MonoBehaviour {
    public float CameraSensitivity = 90;
    public float YAxisMoveSpeed = 8;
    public float NormalMoveSpeed = 10;
    public float SlowDownMoveFactor = 0.25f;
    public float SpeedUpMoveFactor = 3;

    private float rotationX;
    private float rotationY;

    private void Start() {
//      LockCursor();
      SetInitialRotationValues();
    }

    private void Update() {
      if (!IsCusorLocked()) {
        TryLockCursor();
        return;
      }

      if (ShouldUnlockCursor()) {
        UnlockCursor();
      }

      RotateCamera();
      MoveCamera();
    }

    private void SetInitialRotationValues() {
      rotationX = transform.rotation.x;
      rotationY = transform.rotation.y;
    }

    private void RotateCamera() {
      CalculateRotation();
      ChangeLocalRotation();
    }

    private void CalculateRotation() {
      rotationX += Input.GetAxis("Mouse X") * CameraSensitivity * Time.deltaTime;
      rotationY += Input.GetAxis("Mouse Y") * CameraSensitivity * Time.deltaTime;
      rotationY = Mathf.Clamp(rotationY, -90, 90);
    }

    private void ChangeLocalRotation() {
      transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
      transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
    }

    private void MoveCamera() {
      var resultSpeed = ResultSpeed();

      transform.position += transform.forward * resultSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
      transform.position += transform.right * resultSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;

      MoveYAxis();
    }

    private void MoveYAxis() {
      if (ShouldGoDown()) {
        transform.position += transform.up * YAxisMoveSpeed * Time.deltaTime;
      }
      else if (ShouldGoUp()) {
        transform.position -= transform.up * YAxisMoveSpeed * Time.deltaTime;
      }
    }

    private float ResultSpeed() {
      var resultSpeed = NormalMoveSpeed;
      if (ShouldSpeedUp()) {
        resultSpeed *= SpeedUpMoveFactor;
      }
      else if (ShouldSlowDown()) {
        resultSpeed *= SlowDownMoveFactor;
      }
      return resultSpeed;
    }

    private static bool ShouldGoUp() {
      return Input.GetKey(KeyCode.E);
    }

    private static bool ShouldGoDown() {
      return Input.GetKey(KeyCode.Q);
    }

    private static bool ShouldSlowDown() {
      return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    }

    private static bool ShouldSpeedUp() {
      return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    private static bool ShouldUnlockCursor() {
      return Input.GetKeyDown(KeyCode.Escape);
    }

    private static void UnlockCursor() {
      Cursor.lockState = CursorLockMode.None;
    }

    private static void TryLockCursor() {
      if (Input.anyKey) {
        LockCursor();
      }
    }

    private static void LockCursor() {
      Cursor.lockState = CursorLockMode.Locked;
    }

    private static bool IsCusorLocked() {
      return Cursor.lockState != CursorLockMode.None;
    }
  }
}
