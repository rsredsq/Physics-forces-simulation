using System;
using Simulation;
using TMPro;
using UnityEngine;

namespace Utils {
  public class EditorController : MonoBehaviour {
    private GameObject chargedObjectToInstantiate;
    private SimulationSystem simulationSystem;
    public Camera CameraObject;
    public Canvas CanvasObject;

    public TMP_InputField Weight;
    public TMP_InputField Speed;
    public TMP_InputField Position;
    public TMP_InputField Charge;

    [SerializeField]
    public bool EditorMode;

    private void Awake() {
      AppManager.Instance.EditorModeEnabled = EditorMode;
      chargedObjectToInstantiate = Resources.Load<GameObject>("Prefabs/ChargedObject");
      simulationSystem = FindObjectOfType<SimulationSystem>();
    }

    private void Update() {
      if (Input.GetKeyDown(KeyCode.Y) && !EditorMode) {
        AppManager.Instance.EditorModeEnabled = !AppManager.Instance.EditorModeEnabled;
      }

      if (Input.GetKeyDown(KeyCode.V) && EditorMode) {
        StartAddChargeObject();
      }

      if (Input.GetKeyDown(KeyCode.P) && EditorMode) {
        Helpers.SaveScene();
      }

      if (Input.GetKeyDown(KeyCode.O)) {
        Helpers.LoadScene();
      }

      if (Input.GetKeyDown(KeyCode.C)) {
        EditorMode = !EditorMode;
        AppManager.Instance.EditorModeEnabled = EditorMode;
      }

      if (Input.GetKeyDown(KeyCode.Escape)) {
        UnlockedCursor();
        LoadScenesOnClick.LoadByIndex(0);
      }
    }

    private void StartAddChargeObject() {
      LockedFlyCam();
      UnlockedCanvas();

      UnlockedCursor();
    }

    private void FinishAddChargeObject() {
      LockedCanvas();

      UnlockedFlyCam();
    }

    private void LockedCanvas() {
      CanvasObject.gameObject.SetActive(false);
    }

    private void UnlockedCanvas() {
      CanvasObject.gameObject.SetActive(true);
    }

    private static void UnlockedCursor() {
      Cursor.lockState = CursorLockMode.None;
    }

    private static void LockedCursor() {
      Cursor.lockState = CursorLockMode.Locked;
    }

    private void LockedFlyCam() {
      var flyCamScrypt = CameraObject.GetComponent<FlyCam>();
      flyCamScrypt.enabled = false;

      CameraObject.clearFlags = CameraClearFlags.SolidColor;
      AppManager.Instance.EditorModeEnabled = true;
    }

    private void UnlockedFlyCam() {
      var flyCamScrypt = CameraObject.GetComponent<FlyCam>();
      flyCamScrypt.enabled = true;

      CameraObject.clearFlags = CameraClearFlags.Skybox;

      if (!EditorMode) {
        AppManager.Instance.EditorModeEnabled = false;
      }
    }

    private void ClearTextInInputFields() {
      Weight.text = String.Empty;
      Speed.text = String.Empty;
      Position.text = String.Empty;
      Charge.text = String.Empty;
    }

    public void ClickButtonOk() {
      var weight = Helpers.GetValidWeight(Helpers.ParseFloat(Weight));

      var speed = Helpers.ParseVector3(Speed);
      var position = Helpers.ParseVector3(Position);

      var charge = Helpers.GetValidCharge(Helpers.ParseFloat(Charge));

      LockedCursor();
      FinishAddChargeObject();
      InstantiateNewObject(weight, speed, position, charge);

      ClearTextInInputFields();
    }

    private void InstantiateNewObject(float weight, Vector3 speed, Vector3 pos, float charge) {
      var obj = Instantiate(chargedObjectToInstantiate, simulationSystem.transform);
      var chargedObj = obj.GetComponent<ChargedObject>();
      obj.transform.position = pos;

      chargedObj.Charge = charge;
      chargedObj.Rigidbody.mass = weight;
      chargedObj.startVelocity = speed;

      simulationSystem.AddNewChargedObject(chargedObj);
    }
  }
}
