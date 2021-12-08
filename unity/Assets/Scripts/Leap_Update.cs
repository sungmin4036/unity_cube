using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using System.Runtime.Versioning;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Globalization;
using System.Diagnostics;

public class Leap_Update : MonoBehaviour
{

    Controller controller;


    public GameObject cube;
    public List<Transform> cubeAll;

    public bool move = true;
    public int __UB = 0;
    public int __F = 0;
    public int __DL = 0;
    public int __R = 0;
    public int RU, RB, RD, RF, LU, LB, LD, LF, FU, FD, BD, BU;
    private int R_ent = 0;
    private int U_ent = 0;
    private int F_ent = 0;
    private List<Vector3> start_frame = new List<Vector3>();
    private List<Vector3> startVectorAll = new List<Vector3>();
    private List<Vector3> freeVectorAll = new List<Vector3>();
    private List<Quaternion> freeRotationAll = new List<Quaternion>();

    private float timer;
    private float waitingTime;
    private int clockwiseR = 0;
    private int clockwiseL = 0;
    private int clockwiseU = 0;
    private int clockwiseD = 0;
    private int clockwiseF = 0;
    private int clockwiseB = 0;
    private int clockwiseR_ent = 0;
    private int clockwiseU_ent = 0;
    private int clockwiseF_ent = 0;



    private List<Transform> cubeFront
    {
        get { return cubeAll.FindAll(F => Mathf.Round(F.transform.position.z) == 0); }
    }
    private List<Transform> cubeBack
    {
        get { return cubeAll.FindAll(B => Mathf.Round(B.transform.position.z) == 2); }
    }
    private List<Transform> cubeLeft
    {
        get { return cubeAll.FindAll(L => Mathf.Round(L.transform.position.x) == 0); }
    }
    private List<Transform> cubeRight
    {
        get { return cubeAll.FindAll(R => Mathf.Round(R.transform.position.x) == 2); }
    }
    private List<Transform> cubeBottom
    {
        get { return cubeAll.FindAll(B => Mathf.Round(B.transform.position.y) == 0); }
    }
    private List<Transform> cubeTop
    {
        get { return cubeAll.FindAll(T => Mathf.Round(T.transform.position.y) == 2); }
    }
    void Start()
    {
        timer = 0.0f;
        waitingTime = 0.2f;
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitingTime)
        {
            //D 회전
            if (clockwiseD == 1 && RD == 0)
            {
                if (__DL == 1)
                {
                    StartCoroutine(RotationCube(cubeBottom, Vector3.down));
                    clockwiseD = 0;
                }


            }
            else if (clockwiseD == 2 && FD == 0)
            {
                if (__DL == 1)
                {
                    StartCoroutine(RotationCube(cubeBottom, Vector3.up));
                    clockwiseD = 0;
                }

            }

            //F 회전
            else if (clockwiseF == 1 && FD == 0)
            {
                if (__F == 1)
                {
                    StartCoroutine(RotationCube(cubeFront, Vector3.back));
                    clockwiseF = 0;
                }

            }
            else if (clockwiseF == 2 && RF == 0)
            {
                if (__F == 1)
                {
                    StartCoroutine(RotationCube(cubeFront, Vector3.forward));
                    clockwiseF = 0;
                }
            }

            // B 회전
            else if (clockwiseB == 1 && RB == 0)
            {
                if (__UB == 1)
                {
                    StartCoroutine(RotationCube(cubeBack, Vector3.forward));
                    clockwiseB = 0;
                }


            }
            else if (clockwiseB == 2 && BD == 0)
            {
                if (__UB == 1)
                {
                    StartCoroutine(RotationCube(cubeBack, Vector3.back));
                    clockwiseB = 0;
                }
            }

            // R 회전
            else if (clockwiseR == 1 && RF == 0)
            {
                if (__R == 1)
                {
                    StartCoroutine(RotationCube(cubeRight, Vector3.right));
                    clockwiseR = 0;
                }


            }
            else if (clockwiseR == 2 && RD == 0)
            {
                if (__R == 1)
                {
                    StartCoroutine(RotationCube(cubeRight, Vector3.left));
                    clockwiseR = 0;
                }

            }

            // L 회전
            else if (clockwiseL == 1 && LD == 0)
            {
                if (__DL == 1)
                {
                    StartCoroutine(RotationCube(cubeLeft, Vector3.left));
                    clockwiseL = 0;
                }


            }
            else if (clockwiseL == 2 && LF == 0)
            {
                if (__DL == 1)
                {
                    StartCoroutine(RotationCube(cubeLeft, Vector3.right));
                    clockwiseL = 0;
                }

            }

            // U 회전
            else if (clockwiseU == 1 && FU == 0)
            {
                if (__UB == 1)
                {
                    StartCoroutine(RotationCube(cubeTop, Vector3.up));
                    clockwiseU = 0;
                }


            }
            else if (clockwiseU == 2 && RU == 0)
            {
                if (__UB == 1)
                {
                    StartCoroutine(RotationCube(cubeTop, Vector3.down));
                    clockwiseU = 0;
                }

            }
            
            // X 회전
            else if (clockwiseU_ent == 1 && F_ent== 1)
            {
                StartCoroutine(RotationCube(cubeAll, Vector3.left));
                clockwiseU_ent = 0;
            }
            else if (clockwiseF_ent == 1 && U_ent == 1)
            {
                //sX
                StartCoroutine(RotationCube(cubeAll, Vector3.right));
                clockwiseF_ent = 0;

            }

            // Y 회전
            else if (clockwiseF_ent == 1 && R_ent == 1)
            {
                //y
                StartCoroutine(RotationCube(cubeAll, Vector3.down));
                clockwiseF_ent = 0;

            }
            else if (clockwiseR_ent == 1 && F_ent == 1)
            {
                //sY
                StartCoroutine(RotationCube(cubeAll, Vector3.up));
                clockwiseR_ent = 0;

            }

            // Z 회전
            else if (clockwiseR_ent == 1 && U_ent == 1)
            {
                //z     
                StartCoroutine(RotationCube(cubeAll, Vector3.forward));
                clockwiseR_ent = 0;

            }
            else if (clockwiseU_ent == 1 && R_ent == 1)
            {
                //sZ
                StartCoroutine(RotationCube(cubeAll, Vector3.back));
                clockwiseU_ent = 0;

            }

            if (FD + RD + BD + LD == 3)
            {
                clockwiseB = 0;
                clockwiseF = 0;
                clockwiseU = 0;
                clockwiseR = 0;
                clockwiseL = 0;
                clockwiseU_ent = 0;
                clockwiseF_ent = 0;
                clockwiseR_ent = 0;

                if (FD == 0)
                {
                    clockwiseD = 1;
                }
                if (RD == 0)
                {
                    clockwiseD = 2;
                }
            }

            else if (FU + RF + FD + LF == 3)
            {
                clockwiseB = 0;
                clockwiseD = 0;
                clockwiseU = 0;
                clockwiseR = 0;
                clockwiseL = 0;
                clockwiseU_ent = 0;
                clockwiseF_ent = 0;
                clockwiseR_ent = 0;

                if (RF == 0)
                {
                    clockwiseF = 1;
                }
                if (FD == 0)
                {
                    clockwiseF = 2;
                }

            }

            else if (BU + LB + BD + RB == 3)
            {
                clockwiseD = 0;
                clockwiseF = 0;
                clockwiseU = 0;
                clockwiseR = 0;
                clockwiseL = 0;
                clockwiseU_ent = 0;
                clockwiseF_ent = 0;
                clockwiseR_ent = 0;
                if (BD == 0)
                {
                    clockwiseB = 1;
                }
                if (RB == 0)
                {
                    clockwiseB = 2;
                }

            }

            else if (RU + RB + RD + RF == 3)
            {
                clockwiseB = 0;
                clockwiseF = 0;
                clockwiseU = 0;
                clockwiseD = 0;
                clockwiseL = 0;
                clockwiseU_ent = 0;
                clockwiseF_ent = 0;
                clockwiseR_ent = 0;
                if (RD == 0)
                {
                    clockwiseR = 1;
                }
                if (RF == 0)
                {
                    clockwiseR = 2;
                }

            }

            else if (LU + LF + LD + LB == 3)
            {
                clockwiseB = 0;
                clockwiseF = 0;
                clockwiseU = 0;
                clockwiseR = 0;
                clockwiseD = 0;
                clockwiseU_ent = 0;
                clockwiseF_ent = 0;
                clockwiseR_ent = 0;
                if (LF == 0)
                {
                    clockwiseL = 1;
                }
                if (LD == 0)
                {
                    clockwiseL = 2;
                }


            }

            else if (FU + LU + BU + RU == 3)
            {
                clockwiseB = 0;
                clockwiseF = 0;
                clockwiseD = 0;
                clockwiseR = 0;
                clockwiseL = 0;
                clockwiseU_ent = 0;
                clockwiseF_ent = 0;
                clockwiseR_ent = 0;
                if (RU == 0)
                {
                    clockwiseU = 1;
                }
                if (FU == 0)
                {
                    clockwiseU = 2;

                }
            }


            // 전체회전 
            
            else if (LU + LF + LD + LB + RU + RB + RD + RF + FU + BU + FD + BD <= 1 && R_ent + U_ent + F_ent == 1)
            {
                clockwiseR = 0;
                clockwiseL = 0;
                clockwiseU = 0;
                clockwiseD = 0;
                clockwiseF = 0;
                clockwiseB = 0;

                if (R_ent == 1)
                {
                    clockwiseR_ent = 1;
                }
                else if (U_ent == 1)
                {
                    clockwiseU_ent = 1;
                }
                else if (F_ent == 1)
                {
                    clockwiseF_ent = 1;
                }


                
            }



            timer = 0;
        }

    }

    public void RU_on()
    {
        RU = 1;
    }
    public void RU_off()
    {
        RU = 0;
    }
    public void RB_on()
    {
        RB = 1;

    }
    public void RB_off()
    {
        RB = 0;

    }
    public void RF_on()
    {
        RF = 1;

    }
    public void RF_off()
    {
        RF = 0;

    }
    public void RD_on()
    {
        RD = 1;

    }
    public void RD_off()
    {
        RD = 0;

    }
    public void LU_on()
    {
        LU = 1;
    }
    public void LU_off()
    {
        LU = 0;
    }
    public void LB_on()
    {
        LB = 1;
    }
    public void LB_off()
    {
        LB = 0;
    }
    public void LD_on()
    {
        LD = 1;
    }
    public void LD_off()
    {
        LD = 0;
    }
    public void LF_on()
    {
        LF = 1;
    }
    public void LF_off()
    {
        LF = 0;
    }
    public void FU_on()
    {
        FU = 1;
    }
    public void FU_off()
    {
        FU = 0;
    }
    public void FD_on()
    {
        FD = 1;
    }
    public void FD_off()
    {
        FD = 0;
    }
    public void BD_on()
    {
        BD = 1;
    }
    public void BD_off()
    {
        BD = 0;
    }
    public void BU_on()
    {
        BU = 1;
    }
    public void BU_off()
    {
        BU = 0;
    }

    public void F_on()
    {
        __F = 1;
    }
    public void F_off()
    {
        __F = 0;
    }
    public void DL_on()
    {
        __DL = 1;
    }
    public void DL_off()
    {
        __DL = 0;
    }
    public void UB_on()
    {
        __UB = 1;
    }
    public void UB_off()
    {
        __UB = 0;
    }
    public void R_on()
    {
        __R = 1;
    }
    public void R_off()
    {
        __R = 0;
    }
    
    public void R_ent_on()
    {
        R_ent = 1;
    }
    public void R_ent_off()
    {
        R_ent = 0;
    }
    public void U_ent_on()
    {
        U_ent = 1;
    }
    public void U_ent_off()
    {
        U_ent = 0;
    }
    public void F_ent_on()
    {
        F_ent = 1;
    }
    public void F_ent_off()
    {
        F_ent = 0;
    }

    public void setFreeCube()
    {
        freeVectorAll.Clear();
        freeRotationAll.Clear();
        for (int i = 0; i < cubeAll.Count; i++)
        {
            freeVectorAll.Add(cubeAll[i].transform.localPosition);
            freeRotationAll.Add(cubeAll[i].transform.rotation);
        }
    }
    public void getFreeCube()
    {
        for (int i = 0; i < cubeAll.Count; i++)
        {
            cubeAll[i].transform.localPosition = freeVectorAll[i];
            cubeAll[i].transform.rotation = freeRotationAll[i];
        }
    }

    IEnumerator RotationCube(List<Transform> list, Vector3 v3)
    {
        int count = 0;
        move = true;
        while (true)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].RotateAround(Vector3.one, v3, Mathf.RoundToInt(5.0f)); //Mathf.Rad2Deg
            }
            count++;
            if (count >= 18.0f)
            {
                move = false;
                break;
            }
            yield return null;
        }
    }
}

