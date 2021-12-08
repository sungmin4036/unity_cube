using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour {
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;

    CubeState cubeState;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
    public void set () {
        cubeState = FindObjectOfType<CubeState>();
        updateMap(cubeState.front, front);
        updateMap(cubeState.back, back);
        updateMap(cubeState.left, left);
        updateMap(cubeState.right, right);
        updateMap(cubeState.up, up);
        updateMap(cubeState.down, down);
    }
    void updateMap (List<GameObject> face, Transform side) {
        int i = 0;
        foreach (Transform map in side) {
            if (face[i].name[5] == 'g') {
                map.GetComponent<Image>().color = Color.green;
            }
            if (face[i].name[5] == 'y') {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if (face[i].name[5] == 'b') {
                if (face[i].name[7] == 'a') {
                    map.GetComponent<Image>().color = Color.grey;
                }
            }
            if (face[i].name[5] == 'r') {
                map.GetComponent<Image>().color = Color.red;
            }
            if (face[i].name[5] == 'p') {
                map.GetComponent<Image>().color = Color.white;
            }
            if (face[i].name[5] == 'b') {
                if (face[i].name[7] == 'u') {
                    map.GetComponent<Image>().color = Color.blue;
                }
            }
            i++;
        }
    }
}