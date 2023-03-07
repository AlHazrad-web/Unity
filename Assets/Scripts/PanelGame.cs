using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelGame : MonoBehaviour {

    GlobalUI _GlobalUI;

    public GameObject _PanelContent;

    public GameObject _PanelEndGame;

    public GameObject _TextWin;
    public GameObject _TextFailure;

    public Text _Score;

   

    public void Init() {
        _PanelContent = gameObject.transform.Find("PanelContent").gameObject;
        _GlobalUI = gameObject.transform.parent.gameObject.transform.parent.GetComponent<GlobalUI>();
        string text = _GlobalUI._GameCore.KilledEnemy + "/" + _GlobalUI._GameCore.MaxKilledEnemy;
        _Score.text = text;
    }


    public void OpenPanel() {
        _PanelContent.SetActive(true);
    }

    public void ClosePanel() {
        _PanelContent.SetActive(false);
    }


    public void AddScore() {
        string text = _GlobalUI._GameCore.KilledEnemy + "/" + _GlobalUI._GameCore.MaxKilledEnemy;
        _Score.text = text;
    }

}
