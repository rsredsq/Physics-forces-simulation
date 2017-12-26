using TMPro;
using UnityEngine;

public class HelpMessage : MonoBehaviour {
  public TMP_Text MessageStorage;

  private int curindex = -1;

  public void InsertMessage(int index) {
    if (index == 1) {
      MessageStorage.GetComponent<TMP_Text>().text =
        "В режиме редактора (по умолчанию) вы можете расставлять шары, задавать им необходимые физические величины. Также вы можете сохранять сцены и загружать ранее созданные.\nЕсли наверху написано [Editor Mode] - значит это режим редактора.";
    }
    else if (index == 2) {
      MessageStorage.GetComponent<TMP_Text>().text =
        "V - добавить шар\nP - сохранить сцену\nO - открыть сцену\nY - остановить физический процесс\nC - войти/выйти из режима редактора\nSPACE - начать следить за шаром / окончить слежение\nESC - выйти в главное меню";
    }

    curindex = index - 1;
  }

  public void InsertCurrentMessage() {
    InsertMessage((curindex + 1) % 2 + 1);
  }
}
