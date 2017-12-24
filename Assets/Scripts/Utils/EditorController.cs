using System;
using Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utils {
  public class EditorController : MonoBehaviour {
    private GameObject chargedObjectToInstantiate;
    private SimulationSystem simulationSystem;
    public Camera CameraObject;
    public Canvas CanvasObject;

    public TMP_InputField Weight;

    public TMP_InputField SpeedX;
    public TMP_InputField SpeedY;
    public TMP_InputField SpeedZ;

    public TMP_InputField Charge;


    private void Awake() {
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
      ;
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
      AppManager.Instance.EditorModeEnabled = false;
    }

    private float GetStringFromInputField(TMP_InputField field) {
      return string.IsNullOrEmpty(field.text)
        ? float.Parse(field.placeholder.GetComponent<TMP_Text>().text)
        : float.Parse(field.text);
    }

    private float GetValidCharge(float charge) {
      return charge * 1e-6f;
    }

    private float GetValidWeight(float weight) {
      return weight <= 0 ? 1 : weight;
    }

    private void ClearTextInInputFields() {
      Weight.text = String.Empty;
      SpeedX.text = String.Empty;
      SpeedY.text = String.Empty;
      SpeedZ.text = String.Empty;
      Charge.text = String.Empty;
    }

    public void ClickButtonOk() {
      var weight = GetStringFromInputField(Weight);
      weight = GetValidWeight(weight);

      var speedX = GetStringFromInputField(SpeedX);
      var speedY = GetStringFromInputField(SpeedY);
      var speedZ = GetStringFromInputField(SpeedZ);

      var charge = GetStringFromInputField(Charge);
      charge = GetValidCharge(charge);

      FinishAddChargeObject();
      InstantiateNewObject(weight, speedX, speedY, speedZ, charge);

      ClearTextInInputFields();
    }

    private void InstantiateNewObject(float weight, float speedX, float speedY, float speedZ, float charge) {
      var obj = Instantiate(chargedObjectToInstantiate, simulationSystem.transform);
      var chargedObj = obj.GetComponent<ChargedObject>();
      var objPosition = transform.position + transform.TransformDirection(Vector3.forward);
      obj.transform.position = objPosition;

      chargedObj.Charge = charge;
      chargedObj.Rigidbody.mass = weight;
      chargedObj.startVelocity = new Vector3(speedX, speedY, speedZ);

      simulationSystem.AddNewChargedObject(chargedObj);
    }
  }
}
