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


    private void Awake() {
      AppManager.Instance.EditorModeEnabled = true;
      chargedObjectToInstantiate = Resources.Load<GameObject>("Prefabs/ChargedObject");
      simulationSystem = FindObjectOfType<SimulationSystem>();
    }

    private void Update() {
      if (Input.GetKeyDown(KeyCode.Y)) {
        AppManager.Instance.EditorModeEnabled = !AppManager.Instance.EditorModeEnabled;
      }

      if (Input.GetKeyDown(KeyCode.V)) {
        StartAddChargeObject();
      }

      if (Input.GetKeyDown(KeyCode.P)) {
        Helpers.SaveScene();
      }

      if (Input.GetKeyDown(KeyCode.O)) {
        Helpers.LoadScene();
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
//      AppManager.Instance.EditorModeEnabled = false;
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
