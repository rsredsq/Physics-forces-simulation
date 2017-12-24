using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Utils {
  public static class Helpers {
    public static float GetValidCharge(float charge) {
      return charge * 1e-6f;
    }

    public static float GetValidWeight(float weight) {
      return weight <= 0 ? 1 : weight;
    }

    public static string StringFromInputField(TMP_InputField field) {
      return string.IsNullOrEmpty(field.text)
        ? field.placeholder.GetComponent<TMP_Text>().text
        : field.text;
    }

    public static float ParseFloat(TMP_InputField field) {
      return float.Parse(StringFromInputField(field));
    }

    public static Vector3 ParseVector3(TMP_InputField field) {
      var value = StringFromInputField(field);
      var vectorVars = value.Split(' ').Select(float.Parse).ToList();
      if (vectorVars.Count != 3) throw new Exception("antoha, eto pizda");
      return new Vector3(vectorVars[0], vectorVars[1], vectorVars[2]);
    }
  }
}
