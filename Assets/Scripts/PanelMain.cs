using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PanelMain : MonoBehaviour {

    GlobalUI _GlobalUI;

    public GameObject _PanelContent;
    public GameObject _MainShips;

    void Start() {
        _PanelContent = gameObject.transform.Find("PanelContent").gameObject;
        _MainShips = GameObject.Find("MainShip").gameObject;
        _GlobalUI = gameObject.transform.parent.gameObject.transform.parent.GetComponent<GlobalUI>();
    }

    public void OpenPanel() {
        _PanelContent.SetActive(true);
        _MainShips.SetActive(true);
    }

    public void ClosePanel() {
        _PanelContent.SetActive(false);
        _MainShips.SetActive(false);
    }

    public void PressButtonStart() {
        _PanelContent.SetActive(false);
        _GlobalUI._GameCore.StartGame();
    }

    public void PressButtonExit() {
        Application.Quit();
    }

    public void PressButtonRestart() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
    }



}
