using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public GameObject btn;
    public float smoothTime = 5.0f;
    public bool isActivate = false;
    private float xv1 = -20.0f;
    private float yv1 = -20.0f;
    private float zv1 = 20.0f;
    private float xv2 = -10.0f;
    private float yv2 = -30.0f;
    private float xv3 = 10.0f;
    private float yv3 = 10.0f;
    private float zv3 = -10.0f;
    private float xv4 = 20.0f;
    private float yv4 = -20.0f;
    private bool t = false;
    // Start is called before the first frame update
    void Start() {

    }
    // Update is called once per frame
    void FixedUpdate() {
        if (isActivate) {
            float positionX = Mathf.SmoothDamp(transform.position.x, -3.5f, ref xv1, smoothTime);
            float positionY = Mathf.SmoothDamp(transform.position.y, 2.0f, ref yv1, smoothTime);
            float positionZ = Mathf.SmoothDamp(transform.position.z, -1.8f, ref zv1, smoothTime);
            float rotationX = Mathf.SmoothDampAngle(transform.rotation.x, 0f, ref xv2, smoothTime);
            float rotationY = Mathf.SmoothDampAngle(transform.rotation.y, -70.0f, ref yv2, -20.0f);
            transform.position = new Vector3(positionX, positionY, positionZ);
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0.0f);
        } else {
            float positionX = Mathf.SmoothDamp(transform.position.x, 6.36f, ref xv3, -10.0f);
            float positionY = Mathf.SmoothDamp(transform.position.y, 5.59f, ref yv3, -10.0f);
            float positionZ = Mathf.SmoothDamp(transform.position.z, -4.66f, ref zv3, -10.0f);
            float rotationX = Mathf.SmoothDampAngle(transform.rotation.x, 25.0f, ref xv4, -10.0f);
            float rotationY = Mathf.SmoothDampAngle(transform.rotation.y, -45.0f, ref yv4, -10.0f);
            transform.position = new Vector3(positionX, positionY, positionZ);
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0.0f);
        }
    }
}