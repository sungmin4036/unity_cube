using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour {
    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;
    public GameObject emptyGO;

    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();

    CubeState cubeState;
    CubeMap cubeMap;
    private int layerMask = 1 << 8;
    // Start is called before the first frame update
    void Start () {
        setRayTransforms();

        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();
        List<GameObject> facesHit = new List<GameObject>();
        Vector3 ray = tFront.transform.position;
        RaycastHit hit;

        // Does the ray intersect any objects in the layerMask?
        if (Physics.Raycast(ray, transform.TransformDirection(Vector3.forward)/*rayTransform.forward*/, out hit, Mathf.Infinity, layerMask)) {
            Debug.DrawRay(ray, transform.TransformDirection(Vector3.forward)/*rayTransform.forward*/ * 1000, Color.yellow);
            facesHit.Add(hit.collider.gameObject);
            //print(hit.collider.gameObject.name);
        } else {
            Debug.DrawRay(ray, transform.TransformDirection(Vector3.forward)/*rayTransform.forward*/ * 1000, Color.green);
        }
        cubeState.up = facesHit;
        cubeMap.set();
    }

    // Update is called once per frame
    void Update () {
        readState();
        /*
        cubeState.up = readFace(upRays, tUp);
        cubeState.down = readFace(downRays, tDown);
        cubeState.left = readFace(leftRays, tLeft);
        cubeState.right = readFace(rightRays, tRight);
        cubeState.front = readFace(frontRays, tFront);
        cubeState.back = readFace(backRays, tBack);
        */
    }
    public void readState () {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        // set the state of each position in the list of sides so we know
        // what color is in what position
        cubeState.up = readFace(upRays, tUp);
        cubeState.down = readFace(downRays, tDown);
        cubeState.left = readFace(leftRays, tLeft);
        cubeState.right = readFace(rightRays, tRight);
        cubeState.front = readFace(frontRays, tFront);
        cubeState.back = readFace(backRays, tBack);

        // update the map with the found positions
        cubeMap.set();
    }
    void setRayTransforms () {
        // populate the ray lists with raycasts eminating from the transform, angled towards the cube.
        upRays = buildRays(tUp, new Vector3(90, 0, 0));
        downRays = buildRays(tDown, new Vector3(270, 0, 0));
        leftRays = buildRays(tLeft, new Vector3(0, 90, 0));
        rightRays = buildRays(tRight, new Vector3(0, 270, 0));
        frontRays = buildRays(tFront, new Vector3(0, 0, 0));
        backRays = buildRays(tBack, new Vector3(0, 180, 0));
    }
    List<GameObject> buildRays (Transform rayTransform, Vector3 direction) {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();
        /* |0|1|2|
         * |3|4|5|
         * |6|7|8|*/
        for (int y = 2; y > -1; y--) {
            for (int x = 0; x < 3; x++) {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x, rayTransform.localPosition.y + y, rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }
    public List<GameObject> readFace (List<GameObject> rayStarts, Transform rayTransform) {
        List<GameObject> facesHit = new List<GameObject>();

        foreach (GameObject rayStart in rayStarts) {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;
            // Does the ray intersect any objects in the layerMask?
            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask)) {
                Debug.DrawRay(ray, rayTransform.forward * 10, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
            } else {
                Debug.DrawRay(ray, rayTransform.forward * 10, Color.green);
            }
        }
        return facesHit;
    }
}