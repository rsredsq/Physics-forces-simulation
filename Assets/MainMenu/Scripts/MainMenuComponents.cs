using UnityEngine;

public class MainMenuComponents : MonoBehaviour {
  public AudioSource ClickSound;
  public AudioSource HoverSound;

  public Animator CanvHelp;
  public Animator CanvMain;

    public void Position(bool state) {
      CanvMain.SetBool("isHidden", state);
      CanvHelp.SetBool("isHidden", state);
  }

  public void PlayHover() {
    HoverSound.Play();
  }

  public void PlayClick() {
    ClickSound.Play();
  }
}
