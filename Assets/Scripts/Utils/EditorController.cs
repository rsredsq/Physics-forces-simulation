using Simulation;
using UnityEngine;

namespace Utils {
  public class EditorController : MonoBehaviour {
    private GameObject chargedObjectToInstantiate;
    private SimulationSystem simulationSystem;

    private void Awake() {
      chargedObjectToInstantiate = Resources.Load<GameObject>("Prefabs/ChargedObject");
      simulationSystem = FindObjectOfType<SimulationSystem>();
    }

    private void Update() {
      if (Input.GetKeyDown(KeyCode.Y)) {
        AppManager.Instance.EditorModeEnabled = !AppManager.Instance.EditorModeEnabled;
      }

      if (Input.GetKeyDown(KeyCode.C)) {
        InstantiateNewObject();
      }
    }

    private void InstantiateNewObject() {
      var obj = Instantiate(chargedObjectToInstantiate, simulationSystem.transform);
      var chargedObj = obj.GetComponent<ChargedObject>();
      var objPosition = transform.position + transform.TransformDirection(Vector3.forward);
      obj.transform.position = objPosition;
      simulationSystem.AddNewChargedObject(chargedObj);
    }
  }
}
