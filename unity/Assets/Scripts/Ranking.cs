using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour {
    private CubeClear cubeClear;
    private Text clearTime;
    private int clearCount = 0;
    private float clearTimeTotal = 0.0f;
    List<Text> clearTimeList;
    // Start is called before the first frame update
    void Start() {
        cubeClear = GameObject.Find("Menu").GetComponent<CubeClear>();
    }

    // Update is called once per frame
    void Update() {
        
    }
    public void setClearTimeText (Text t) {
        clearTime = t;
        clearTimeList.Add(clearTime);
        Debug.Log(clearTime.text);
    }
    public void setClearTimeFloat(float t) {
        clearCount++;
        clearTimeTotal += t;
    }
    private void rankingSetting() {
        clearTimeList.Sort();
        for(int i=0; i<clearTimeList.Count; i++) {
            Text a = clearTimeList[i];
        }
    }
}
