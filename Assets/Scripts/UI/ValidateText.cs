using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ValidateText : MonoBehaviour {
  private Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
  private string text;

  public InputField Input;

  private void OnGUI() {
    text = Input.text;

    if (regex.IsMatch(text)) {
      return;
    }

    text = text.Substring(0, text.Length - 1);
    Input.text = text;
  }
}
