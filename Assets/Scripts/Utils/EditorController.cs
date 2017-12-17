using Simulation;
using UnityEngine;

namespace Utils {
  public class EditorController : MonoBehaviour {
    private GameObject ChargedObjectToInstantiate;
    private FlyCam FlyCam;
    private SimulationSystem SimulationSystem;

    private void Awake() {
      ChargedObjectToInstantiate = Resources.Load<GameObject>("Prefabs/ChargedObject");
      SimulationSystem = FindObjectOfType<SimulationSystem>();
      FlyCam = GetComponent<FlyCam>();
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
      var obj = Instantiate(ChargedObjectToInstantiate, SimulationSystem.transform);
      var chargedObj = obj.GetComponent<ChargedObject>();
      var cameraPosition = FlyCam.transform.position;
      obj.transform.position = cameraPosition;
      SimulationSystem.AddNewChargedObject(chargedObj);
    }
  }
}
