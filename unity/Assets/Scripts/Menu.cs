using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public GameObject freeModeFirst;
    public GameObject formulaModeFirst;
    public GameObject formulaMode;
    public GameObject timer;
    public Text freeModeTimeText;
    public GameObject startMenu;

    private CameraMove cameraMove;
    private CubeClear cubeClear;
    private Cube cube;
    private FormulaManager formulaManager;
    private int count = 0;
    private bool startSetting = false;
    private bool replay = false;
    private bool start = false;
    private bool free = false;
    private bool formula = false;
    private bool setting = false;
    private bool freeModeTutorial = false;
    private bool formulaModeTutorial = false;

    void Start () {
        cube = GameObject.Find("MainCube").GetComponent<Cube>();
        cubeClear = GameObject.Find("Menu").GetComponent<CubeClear>();
        cameraMove = GameObject.Find("Main Camera").GetComponent<CameraMove>();
        formulaManager = GameObject.Find("FormulaMode").GetComponent<FormulaManager>();
    }
    void Update () {
        if (start) {
            if (count >= 20) {
                count = 0;
                start = false;
                startSetting = true;
                return;
            } else if (cube.startRotate() == 0) {
                count++;
            }
        }
        if (free && startSetting && !cube.getMove()) {
            cube.setFreeCube();
            startSetting = false;
        }
        if (formula && startSetting && !cube.getMove()) {
            cube.setFormulaCube();
            startSetting = false;
        }
        if (replay && !start) {
            if (free) {
                cube.getFreeCube();
                replay = false;
            }
            if (formula && !start) {
                cube.getFormulaCube();
                replay = false;
            }
        }
    }
    public void OnClickReplayBtn () {
        replay = true;
        formulaManager.allObjectSetFalse();
    }
    public void OnClickGoToMenuBtn () {
        start = false;
        free = false;
        formula = false;
        cube.getStartCube();
        timer.transform.Find("Timer").gameObject.SetActive(false);
        formulaMode.SetActive(false);
        startMenu.SetActive(true);
    }
    public void OnClickFreeModeBtn () {
        if (formulaMode.activeSelf || setting || cube.move || free) {
            return;
        }
        if (!freeModeTutorial) {
            freeModeFirst.SetActive(true);
            return;
        }
        cube.move = false;
        start = true;
        free = true;
        formula = false;
        cube.setFreeCube();
        timer.transform.Find("Timer").gameObject.SetActive(true);
    }
    public void OnClickFormulaModeBtn () {
        if (timer.transform.Find("Timer").gameObject.activeSelf || setting || formula || cube.move) {
            return;
        }
        if (!formulaModeTutorial) {
            formulaModeFirst.SetActive(true);
            return;
        }
        start = true;
        free = false;
        formula = true;
        cube.move = false;
        cube.setFormulaCube();
        formulaMode.SetActive(true);
    }
    public void OnClickSettingsBtn () {
        setting = true;
    }
    public void OnClickSettingsExitBtn () {
        setting = false;
    }
    public void OnClickExitEnter () {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void OnClickFreeModeTutorialPlay () {
        freeModeTutorial = true;
        start = true;
        free = true;
        formula = false;
        cube.setFreeCube();
        timer.transform.Find("Timer").gameObject.SetActive(true);
    }
    public void OnClickFormulaModeTutorialPlay () {
        formulaModeTutorial = true;
        start = true;
        free = false;
        formula = true;
        cube.setFormulaCube();
    }
    public void OnClickClearResetBtn () {
        cube.getStartCube();
        freeModeTimeText.text = "00:00";
        cubeClear.setClear(false);
        free = false;
        formula = false;
        timer.transform.Find("Timer").gameObject.SetActive(false);
        formulaMode.SetActive(false);
    }
    public void OnClickBtn() {
        if (cameraMove.isActivate) {
            cameraMove.isActivate = false;
        } else {
            cameraMove.isActivate = true;
        }
    }
    public void setFree (bool f) {
        free = f;
    }
    public bool getFree () {
        return free;
    }
    public void setFormula (bool f) {
        formula = f;
    }
    public bool getFormula () {
        return formula;
    }
    public void setStart (bool s) {
        start = s;
    }
    public bool getStart () {
        return start;
    }
}