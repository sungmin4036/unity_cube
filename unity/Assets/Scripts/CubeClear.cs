using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CubeClear : MonoBehaviour {
    public List<Transform> cubeGreen;
    public List<Transform> cubeYellow;
    public List<Transform> cubeBlack;
    public List<Transform> cubeRed;
    public List<Transform> cubePurple;
    public List<Transform> cubeBlue;
    public GameObject clearText;
    public Text clearTimeText;
    public GameObject FormulaMode;

    // private Ranking rank;
    private Cube cube;
    private Menu menu;
    private Timer timer;
    private FormulaManager formulaManager;
    private CubeState cubeState;
    private bool green;
    private bool yellow;
    private bool black;
    private bool red;
    private bool purple;
    private bool blue;
    private bool clear = false;

    void Start () {
        cube = GameObject.Find("MainCube").GetComponent<Cube>();
        formulaManager = GameObject.Find("FormulaMode").GetComponent<FormulaManager>();
        menu = GameObject.Find("Menu").GetComponent<Menu>();
        timer = GameObject.Find("TimerEvent").GetComponent<Timer>();
        cubeState = GameObject.Find("MainCube").GetComponent<CubeState>();
        //rank = GameObject.Find("Ranking").GetComponent<Ranking>();
    }
    // Update is called once per frame
    void Update () {
        if(clear == true) {
            clear = false;
            timer.setClear(true);
            clearText.SetActive(true);
            clearTime();
            cube.getStartCube();
            menu.setFormula(false);
            menu.setFree(false);
            menu.setStart(false);
        }/*
        if ((menu.getFormula() || menu.getFree()) && !menu.getStart() && !cube.move) {
            //
            green = cubeClearTest(cubeGreen);
            yellow = cubeClearTest(cubeYellow);
            black = cubeClearTest(cubeBlack);
            red = cubeClearTest(cubeRed);
            purple = cubeClearTest(cubePurple);
            blue = cubeClearTest(cubeBlue);//
            if (//green && yellow && black && red && purple && blue//cubeClearTest()) {
                FormulaMode.SetActive(false);
                clear = true;
                timer.setClear(true);
                clearText.SetActive(true);
                clearTime();
                cube.getStartCube();
                menu.setFormula(false);
                menu.setFree(false);
                menu.setStart(false);
                cube.move = true;
            }
        }*/
    }
    public bool cubeClearTest () {
        List<int> num = new List<int>();
        for (int i = 0; i < 9; i++) {
            num.Add(i);
        }
        for (int i = 0; i < frontState(num).Length; i++) {
            if (test(frontState(num), num.Count) &&
                test(backState(num), num.Count) &&
                test(upState(num), num.Count) &&
                test(downState(num), num.Count) &&
                test(leftState(num), num.Count) &&
                test(rightState(num), num.Count)) {
                return true;
            }
        }
        return false;
    }
    private void clearTime () {
        float time = timer.getClearTime();
        // rank.setClearTimeFloat(timer.getClearTime());
        int seconds = (int)time % 60;
        int minute = (int)(time / 60.0f);
        clearTimeText.text = string.Format("{0:D2}:{1:D2}", minute, seconds);
        // rank.setClearTimeText(clearTimeText);
    }
    public void setClear (bool c) {
        clear = c;
    }
    public bool getClear () {
        return clear;
    }
    private int[] frontState (List<int> num) {
        int[] count = Enumerable.Repeat<int>(0, 6).ToArray<int>();
        foreach (int i in num) {
            if (cubeState.front[i].name[5] == 'g') {
                count[0]++;
            }
            if (cubeState.front[i].name[5] == 'y') {
                count[1]++;
            }
            if (cubeState.front[i].name[5] == 'b') {
                if (cubeState.front[i].name[7] == 'a') {
                    count[2]++;
                }
            }
            if (cubeState.front[i].name[5] == 'r') {
                count[3]++;
            }
            if (cubeState.front[i].name[5] == 'p') {
                count[4]++;
            }
            if (cubeState.front[i].name[5] == 'b') {
                if (cubeState.front[i].name[7] == 'u') {
                    count[5]++;
                }
            }
        }
        return count;
    }
    private int[] backState (List<int> num) {
        int[] count = Enumerable.Repeat<int>(0, 6).ToArray<int>();
        foreach (int i in num) {
            if (cubeState.back[i].name[5] == 'g') {
                count[0]++;
            }
            if (cubeState.back[i].name[5] == 'y') {
                count[1]++;
            }
            if (cubeState.back[i].name[5] == 'b') {
                if (cubeState.back[i].name[7] == 'a') {
                    count[2]++;
                }
            }
            if (cubeState.back[i].name[5] == 'r') {
                count[3]++;
            }
            if (cubeState.back[i].name[5] == 'p') {
                count[4]++;
            }
            if (cubeState.back[i].name[5] == 'b') {
                if (cubeState.back[i].name[7] == 'u') {
                    count[5]++;
                }
            }
        }
        return count;
    }
    private int[] upState (List<int> num) {
        int[] count = Enumerable.Repeat<int>(0, 6).ToArray<int>();
        foreach (int i in num) {
            if (cubeState.up[i].name[5] == 'g') {
                count[0]++;
            }
            if (cubeState.up[i].name[5] == 'y') {
                count[1]++;
            }
            if (cubeState.up[i].name[5] == 'b') {
                if (cubeState.up[i].name[7] == 'a') {
                    count[2]++;
                }
            }
            if (cubeState.up[i].name[5] == 'r') {
                count[3]++;
            }
            if (cubeState.up[i].name[5] == 'p') {
                count[4]++;
            }
            if (cubeState.up[i].name[5] == 'b') {
                if (cubeState.up[i].name[7] == 'u') {
                    count[5]++;
                }
            }
        }
        return count;
    }
    private int[] downState (List<int> num) {
        int[] count = Enumerable.Repeat<int>(0, 6).ToArray<int>();
        foreach (int i in num) {
            if (cubeState.down[i].name[5] == 'g') {
                count[0]++;
            }
            if (cubeState.down[i].name[5] == 'y') {
                count[1]++;
            }
            if (cubeState.down[i].name[5] == 'b') {
                if (cubeState.down[i].name[7] == 'a') {
                    count[2]++;
                }
            }
            if (cubeState.down[i].name[5] == 'r') {
                count[3]++;
            }
            if (cubeState.down[i].name[5] == 'p') {
                count[4]++;
            }
            if (cubeState.down[i].name[5] == 'b') {
                if (cubeState.down[i].name[7] == 'u') {
                    count[5]++;
                }
            }
        }
        return count;
    }
    private int[] leftState (List<int> num) {
        int[] count = Enumerable.Repeat<int>(0, 6).ToArray<int>();
        foreach (int i in num) {
            if (cubeState.left[i].name[5] == 'g') {
                count[0]++;
            }
            if (cubeState.left[i].name[5] == 'y') {
                count[1]++;
            }
            if (cubeState.left[i].name[5] == 'b') {
                if (cubeState.left[i].name[7] == 'a') {
                    count[2]++;
                }
            }
            if (cubeState.left[i].name[5] == 'r') {
                count[3]++;
            }
            if (cubeState.left[i].name[5] == 'p') {
                count[4]++;
            }
            if (cubeState.left[i].name[5] == 'b') {
                if (cubeState.left[i].name[7] == 'u') {
                    count[5]++;
                }
            }
        }
        return count;
    }
    private int[] rightState (List<int> num) {
        int[] count = Enumerable.Repeat<int>(0, 6).ToArray<int>();
        foreach (int i in num) {
            if (cubeState.right[i].name[5] == 'g') {
                count[0]++;
            }
            if (cubeState.right[i].name[5] == 'y') {
                count[1]++;
            }
            if (cubeState.right[i].name[5] == 'b') {
                if (cubeState.right[i].name[7] == 'a') {
                    count[2]++;
                }
            }
            if (cubeState.right[i].name[5] == 'r') {
                count[3]++;
            }
            if (cubeState.right[i].name[5] == 'p') {
                count[4]++;
            }
            if (cubeState.right[i].name[5] == 'b') {
                if (cubeState.right[i].name[7] == 'u') {
                    count[5]++;
                }
            }
        }
        return count;
    }
    private bool test (int[] count, int num) {
        for (int i = 0; i < count.Length; i++) {
            if (count[i] == num) {
                return true;
            }
        }
        return false;
    }
}