using Simulation;
using UnityEngine;

namespace Utils {
  public class EditorController : MonoBehaviour {
    public GameObject ObjectToInstantiate;
    private FlyCam FlyCam;
    public SimulationSystem SimulationSystem;

    private void Awake() {
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
      var obj = Instantiate(ObjectToInstantiate, SimulationSystem.transform);
      var chargedObj = obj.GetComponent<ChargedObject>();
      var cameraPosition = FlyCam.transform.position;
      obj.transform.position = cameraPosition;
      SimulationSystem.AddNewChargedObject(chargedObj);
    }
  }
}
