using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Permissions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour {
    public Text timeText;
    public Menu menu;

    private bool clear = false;
    private bool saveTime = false;
    private float clearTime;
    private float time;
    private int seconds;
    private int minute;

    // Start is called before the first frame update
    void Start () {
        menu = GameObject.Find("Menu").GetComponent<Menu>();
    }

    // Update is called once per frame
    void Update () {
        if (clear) {
            setCleartime(time);
            time = 0;
            clear = false;
            menu.setFree(false);
            menu.setFormula(false);
            menu.setStart(false);
        }
        if (!menu.getFree()) {

        } else if (menu.getFree() && !menu.getStart() && !clear) {
            time += Time.deltaTime;
            seconds = (int)time % 60;
            minute = (int)(time / 60.0f);
            timeText.text = string.Format("{0:D2}:{1:D2}", minute, seconds);
        }
    }
    private void setCleartime (float t) {
        if (!saveTime) {
            clearTime = t;
            saveTime = true;
        }
    }
    public float getClearTime () {
        return time;
    }
    public void setClear (bool c) {
        clear = c;
    }
    public bool getClear () {
        return clear;
    }
    public void timerReset () {
        time = 0;
    }
}