using System;
using System.IO;
using System.Linq;
using SFB;
using Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Utils {
  public static class Helpers {
    public static ChargedObjectData[] GlobalData;

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
      var vectorVars = value.Trim().Split(' ').Select(float.Parse).ToList();
      if (vectorVars.Count != 3) throw new Exception("antoha, eto pizda");
      return new Vector3(vectorVars[0], vectorVars[1], vectorVars[2]);
    }

    public static void SaveScene() {
      var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "json");

      var objectsToSave = GameObject.FindObjectsOfType<ChargedObject>()
        .Select(it => it.Data).ToArray();

      var json = JsonHelper.ToJson(objectsToSave);
      var file = File.CreateText(path);

      file.Write(json);
      file.Close();
    }

    public static ChargedObjectData[] LoadData() {
      var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "json", false);

      var path = paths[0];
      if (path.StartsWith("file://")) {
        path = path.Replace("file://", "");
      }

      var json = File.ReadAllText(path);
      var chargedObjectsData = JsonHelper.FromJson<ChargedObjectData>(json);

      return chargedObjectsData;
    }

    static Helpers() {
      SceneManager.sceneLoaded += (scene, mode) => {
        var simulationSystem = GameObject.FindGameObjectWithTag("SimulationSystem");
        var objToCreate = Resources.Load<GameObject>("Prefabs/ChargedObject");

        foreach (var data in GlobalData) {
          var gameObject =
            Object.Instantiate(objToCreate, data.Position, Quaternion.identity, simulationSystem.transform);
          var chargedObject = gameObject.GetComponent<ChargedObject>();
          chargedObject.Data = data;
        }
      };
    }

    public static void LoadScene() {
      var datas = LoadData();
      GlobalData = datas;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }

  public static class JsonHelper {
    public static T[] FromJson<T>(string json) {
      Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
      return wrapper.Items;
    }

    public static string ToJson<T>(T[] array) {
      Wrapper<T> wrapper = new Wrapper<T>();
      wrapper.Items = array;
      return JsonUtility.ToJson(wrapper, true);
    }

    [Serializable]
    private class Wrapper<T> {
      public T[] Items;
    }
  }
}
