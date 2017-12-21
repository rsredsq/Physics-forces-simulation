using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {
    public EventSystem EventS;
    public GameObject SelectedObject;

    private bool buttonSelected;

    public void SetButtonSelected(bool state) {
        buttonSelected = state;
    }

    public void Update() {
        if ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && buttonSelected == false) {
            EventS.SetSelectedGameObject(SelectedObject);
            SetButtonSelected(true);
        }
    }

    private void OnDisable() {
        SetButtonSelected(false);
    }
}