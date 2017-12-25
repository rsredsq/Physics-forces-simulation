using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using SFB;
using Simulation;
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

    public static void SaveScene() {
      var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "");

      var objectsToSave = GameObject.FindObjectsOfType<ChargedObject>()
        .Select(it => it.Data).ToArray();

      var json = JsonHelper.ToJson(objectsToSave);
      var file = File.CreateText(path);

      file.Write(json);
      file.Close();
    }

    public static ChargedObjectData[] LoadScene() {
      var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);

      var path = paths[0];
      if (path.StartsWith("file://")) {
        path = path.Replace("file://", "");
      }

      var json = File.ReadAllText(path);
      var chargedObjectsData = JsonHelper.FromJson<ChargedObjectData>(json);

      return chargedObjectsData;
    }
  }

  public static class JsonHelper {

    public static T[] FromJson<T>(string json) {
      Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
      return wrapper.Items;
    }

    public static string ToJson<T>(T[] array) {
      Wrapper<T> wrapper = new Wrapper<T>();
      wrapper.Items = array;
      return UnityEngine.JsonUtility.ToJson(wrapper);
    }

    [Serializable]
    private class Wrapper<T> {
      public T[] Items;
    }
  }
}
