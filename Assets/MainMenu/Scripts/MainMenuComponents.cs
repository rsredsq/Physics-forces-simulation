using UnityEngine;

public class MainMenuComponents : MonoBehaviour {
    public AudioSource ClickSound;
    public AudioSource HoverSound;

    public Animator CameraObject;

    public void Position(int position) {
        CameraObject.SetFloat("Animate", position);
    }

    public void PlayHover() {
        HoverSound.Play();
    }

    public void PlayClick() {
        ClickSound.Play();
    }
}