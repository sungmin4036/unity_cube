using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Data;
using System.Net;
using System.Threading;
using System;
using System.ComponentModel.Design;

public class Cube : MonoBehaviour {
    public GameObject cube;
    public GameObject keyboard;
    public List<Transform> cubeAll;

    public int f = 0;
    public int b = 0;
    public int u = 0;
    public int d = 0;
    public int l = 0;
    public int r = 0;
    public int x = 0;
    public int y = 0;
    public int z = 0;
    public int sf = 0;
    public int sb = 0;
    public int su = 0;
    public int sd = 0;
    public int sl = 0;
    public int sr = 0;
    public int sx = 0;
    public int sy = 0;
    public int sz = 0;
    public bool move = true;
    private Menu menu;
    private List<Vector3> startVectorAll = new List<Vector3>();
    private List<Quaternion> startRotationAll = new List<Quaternion>();
    private List<Vector3> freeVectorAll = new List<Vector3>();
    private List<Quaternion> freeRotationAll = new List<Quaternion>();
    private List<Vector3> formulaVectorAll = new List<Vector3>();
    private List<Quaternion> formulaRotationAll = new List<Quaternion>();

    private List<Transform> cubeFront {
        get { return cubeAll.FindAll(F => Mathf.Round(F.transform.position.z) == 0); }
    }
    private List<Transform> cubeBack {
        get { return cubeAll.FindAll(B => Mathf.Round(B.transform.position.z) == 2); }
    }
    private List<Transform> cubeLeft {
        get { return cubeAll.FindAll(L => Mathf.Round(L.transform.position.x) == 0); }
    }/*
    public List<Transform> cubeVertical { // 세로
        get { return cubeAll.FindAll(V => Mathf.Round(V.transform.position.x) == 1); }
    }*/
    private List<Transform> cubeRight {
        get { return cubeAll.FindAll(R => Mathf.Round(R.transform.position.x) == 2); }
    }
    private List<Transform> cubeBottom {
        get { return cubeAll.FindAll(B => Mathf.Round(B.transform.position.y) == 0); }
    }/*
    public List<Transform> cubeHorizontal { // 가로
        get { return cubeAll.FindAll(H => Mathf.Round(H.transform.position.y) == 1); }
    }*/
    private List<Transform> cubeTop {
        get { return cubeAll.FindAll(T => Mathf.Round(T.transform.position.y) == 2); }
    }
    // Start is called before the first frame update
    void Start () {
        setStartCube();
        menu = GameObject.Find("Menu").GetComponent<Menu>();
    }

    // Update is called once per frame
    void Update () {
        if (move == true || (!keyboard.activeSelf || menu.getStart())) return;
        if (Input.GetKeyDown(KeyCode.L) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && sl >= 0) {
            StartCoroutine(RotationCube(cubeLeft, Vector3.right));
            sl++;
        } else if (Input.GetKeyDown(KeyCode.U) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && u >= 0) {
            StartCoroutine(RotationCube(cubeTop, Vector3.up));
            u++;
        } else if (Input.GetKeyDown(KeyCode.U) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && su >= 0) {
            StartCoroutine(RotationCube(cubeTop, Vector3.down));
            su++;
        } else if (Input.GetKeyDown(KeyCode.R) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && r >= 0) {
            StartCoroutine(RotationCube(cubeRight, Vector3.right));
            r++;
        } else if (Input.GetKeyDown(KeyCode.L) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && l >= 0) {
            StartCoroutine(RotationCube(cubeLeft, Vector3.left));
            l++;
        } else if (Input.GetKeyDown(KeyCode.D) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && sd >= 0) {
            StartCoroutine(RotationCube(cubeBottom, Vector3.up));
            sd++;
        } else if (Input.GetKeyDown(KeyCode.D) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && d >= 0) {
            StartCoroutine(RotationCube(cubeBottom, Vector3.down));
            d++;
        } else if (Input.GetKeyDown(KeyCode.R) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && sr >= 0) {
            StartCoroutine(RotationCube(cubeRight, Vector3.left));
            sr++;
        } else if (Input.GetKeyDown(KeyCode.F) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && sf >= 0) {
            StartCoroutine(RotationCube(cubeFront, Vector3.forward));
            sf++;
        } else if (Input.GetKeyDown(KeyCode.F) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && f >= 0) {
            StartCoroutine(RotationCube(cubeFront, Vector3.back));
            f++;
        } else if (Input.GetKeyDown(KeyCode.B) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && b >= 0) {
            StartCoroutine(RotationCube(cubeBack, Vector3.forward));
            b++;
        } else if (Input.GetKeyDown(KeyCode.B) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && sb >= 0) {
            StartCoroutine(RotationCube(cubeBack, Vector3.back));
            sb++;
        } else if (Input.GetKeyDown(KeyCode.X) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && x >= 0) {
            StartCoroutine(RotationCube(cubeAll, Vector3.left));
            x++;

        } else if (Input.GetKeyDown(KeyCode.X) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && sx >= 0) {
            StartCoroutine(RotationCube(cubeAll, Vector3.right));
            sx++;
        } else if (Input.GetKeyDown(KeyCode.Y) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && sy >= 0) {
            StartCoroutine(RotationCube(cubeAll, Vector3.up));
            sy++;
        } else if (Input.GetKeyDown(KeyCode.Y) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && y >= 0) {
            StartCoroutine(RotationCube(cubeAll, Vector3.down));
            y++;
        } else if (Input.GetKeyDown(KeyCode.Z) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && z >= 0) {
            StartCoroutine(RotationCube(cubeAll, Vector3.forward));
            z++;
        } else if (Input.GetKeyDown(KeyCode.Z) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && sz >= 0) {
            StartCoroutine(RotationCube(cubeAll, Vector3.back));
            sz++;
        }
    }
    private void setStartCube () {
        for (int i = 0; i < cubeAll.Count; i++) {
            startVectorAll.Add(cubeAll[i].transform.localPosition);
            startRotationAll.Add(cubeAll[i].transform.rotation);
        }
    }/*
    public void setL(bool left) {
        l = left;
    }
    public void 
    public void setSl (bool shiftLeft) {
        sl = shiftLeft;
    }
    public bool getL() {
        return l;
    }*/ // 나중에좀 해놓자
    public void getStartCube () {
        for (int i = 0; i < cubeAll.Count; i++) {
            cubeAll[i].transform.localPosition = startVectorAll[i];
            cubeAll[i].transform.rotation = startRotationAll[i];
        }
    }
    public bool getMove () {
        return move;
    }
    public void setFreeCube () {
        freeVectorAll.Clear();
        freeRotationAll.Clear();
        for (int i = 0; i < cubeAll.Count; i++) {
            freeVectorAll.Add(cubeAll[i].transform.localPosition);
            freeRotationAll.Add(cubeAll[i].transform.rotation);
        }
    }
    public void getFreeCube () {
        for (int i = 0; i < cubeAll.Count; i++) {
            cubeAll[i].transform.localPosition = freeVectorAll[i];
            cubeAll[i].transform.rotation = freeRotationAll[i];
        }
    }
    public void setFormulaCube () {
        formulaVectorAll.Clear();
        formulaRotationAll.Clear();
        for (int i = 0; i < cubeAll.Count; i++) {
            formulaVectorAll.Add(cubeAll[i].transform.localPosition);
            formulaRotationAll.Add(cubeAll[i].transform.rotation);
        }
    }
    public void getFormulaCube () {
        for (int i = 0; i < cubeAll.Count; i++) {
            cubeAll[i].transform.localPosition = formulaVectorAll[i];
            cubeAll[i].transform.rotation = formulaRotationAll[i];
        }
    }
    public void resetCubeBlock () {
        for (int i = 0; i < cubeAll.Count; i++) {
            cubeAll[i].transform.localPosition = startVectorAll[i];
            cubeAll[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public int startRotate () {
        int random = UnityEngine.Random.Range(0, 12);
        if (move == true) {
            return -1;
        } else {
            switch (random) {
                case 0:
                    StartCoroutine(RotationCube(cubeTop, Vector3.up));
                    break;
                case 1:
                    StartCoroutine(RotationCube(cubeTop, Vector3.down));
                    break;
                case 2:
                    StartCoroutine(RotationCube(cubeLeft, Vector3.left));
                    break;
                case 3:
                    StartCoroutine(RotationCube(cubeLeft, Vector3.right));
                    break;
                case 4:
                    StartCoroutine(RotationCube(cubeRight, Vector3.left));
                    break;
                case 5:
                    StartCoroutine(RotationCube(cubeRight, Vector3.right));
                    break;
                case 6:
                    StartCoroutine(RotationCube(cubeBottom, Vector3.up));
                    break;
                case 7:
                    StartCoroutine(RotationCube(cubeBottom, Vector3.down));
                    break;
                case 8:
                    StartCoroutine(RotationCube(cubeFront, Vector3.forward));
                    break;
                case 9:
                    StartCoroutine(RotationCube(cubeFront, Vector3.back));
                    break;
                case 10:
                    StartCoroutine(RotationCube(cubeBack, Vector3.forward));
                    break;
                case 11:
                    StartCoroutine(RotationCube(cubeBack, Vector3.back));
                    break;
            }
            return 0;
        }
    }
    IEnumerator RotationCube (List<Transform> list, Vector3 v3) {
        int count = 0;
        move = true;
        while (true) {
            for (int i = 0; i < list.Count; i++) {
                list[i].RotateAround(Vector3.one, v3, Mathf.RoundToInt(5.0f)); //Mathf.Rad2Deg
            }
            count++;
            if (count >= 18.0f) {
                move = false;
                break;
            }
            yield return null;
        }
    }
}