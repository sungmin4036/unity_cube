using JetBrains.Annotations;
using QuantumTek.QuantumUI;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Policy;
using System.Threading;
using UnityEngine;

public class FormulaManager : MonoBehaviour {
    private Cube cube;
    private CubeClear cubeClear;
    private CubeState cubeState;
    private Menu menu;
    private List<Transform> firstStepcube;
    private int direction; // front 1, back 2, up 3, down 4, right 5, left 6
    private int number;
    private int formulaNum;
    private int y4 = 0;
    private List<int> firstStepFormula = new List<int>();

    public bool firstStep = false;
    public bool secondStep = false;
    public bool thirdStep = false;
    public bool fourthStep = false;
    public bool fifthStep = false;
    public bool sixthStep = false;
    public bool seventhStep = false;
    public bool eighthStep = false;
    public bool step2start = false;
    public bool ready = false;

    public GameObject formulaMode;
    public GameObject step1;
    public GameObject step2;
    public GameObject step3;
    public GameObject step4;
    public GameObject step5;
    public GameObject step6;
    public GameObject step7;
    public GameObject step8;
    public GameObject overallRotation;
    List<int> num = new List<int>();
    void Start () {
        cube = GameObject.Find("MainCube").GetComponent<Cube>();
        cubeClear = GameObject.Find("Menu").GetComponent<CubeClear>();
        menu = GameObject.Find("Menu").GetComponent<Menu>();
        cubeState = GameObject.Find("MainCube").GetComponent<CubeState>();
    }
    void Update () {
        if (!cube.move && !menu.getStart()) {
            if (!firstStep) {
                firstStepTest();
            } else if (!secondStep) {
                secondStepTest();
            } else if (!thirdStep) {
                thirdStepTest();
            } else if (!fourthStep) {
                fourthStepTest();
            } else if (!fifthStep) {
                fifthStepTest();
            } else if (!sixthStep) {
                sixthStepTest();
            } else if (!seventhStep) {
                seventhSteptest();
            } else if (!eighthStep) {
                eighthSteptest();
            }
        }
    }
    public void firstStepTest () {
        if (firstStep) {
            return;
        }
        step1.SetActive(true);
        allSetZero();
        List<int> num = new List<int>();
        num.Add(1); num.Add(3); num.Add(4); num.Add(5); num.Add(7);
        if (direction == 0) {
            if (test(upState(num), num.Count)) {
                allSetFalse();
                number = 0;
                direction = 1;
            } else if (test(downState(num), num.Count)) {
                allSetFalse();
                number = 1;
                direction = 1;
            } else if (test(frontState(num), num.Count)) {
                allSetFalse();
                number = 2;
                direction = 1;
            } else if (test(backState(num), num.Count)) {
                allSetFalse();
                number = 3;
                direction = 1;
            } else if (test(leftState(num), num.Count)) {
                allSetFalse();
                number = 4;
                direction = 1;
            } else if (test(rightState(num), num.Count)) {
                allSetFalse();
                number = 5;
                direction = 1;
            }
        } else if (direction == 1) {
            if (number == 0) {
                direction = 0;
                number = 0;
                firstStep = true;
                allSetFalse();
                return;
            } else if (number == 1) {
                step1.transform.Find("Ex").gameObject.SetActive(false);
                overallRotation.transform.Find("X2").gameObject.SetActive(true);
                if (cube.x == -1) {
                    cube.x = 0;
                } else if (cube.x >= 2) {
                    overallRotation.transform.Find("X2").gameObject.SetActive(false);
                    cube.x = -1;
                    direction = 0;
                    number = 0;
                    firstStep = true;
                    return;
                }
            } else if (number == 2) {
                step1.transform.Find("Ex").gameObject.SetActive(false);
                overallRotation.transform.Find("ShiftX").gameObject.SetActive(true);
                if (cube.sx == -1) {
                    cube.sx = 0;
                } else if (cube.sx >= 1) {
                    overallRotation.transform.Find("ShiftX").gameObject.SetActive(false);
                    cube.sx = -1;
                    direction = 0;
                    number = 0;
                    firstStep = true;
                    return;
                }
            } else if (number == 3) {
                step1.transform.Find("Ex").gameObject.SetActive(false);
                overallRotation.transform.Find("X").gameObject.SetActive(true);
                allSetFalse();
                if (cube.x == -1) {
                    cube.x = 0;
                } else if (cube.x >= 1) {
                    overallRotation.transform.Find("X").gameObject.SetActive(false);
                    cube.x = -1;
                    direction = 0;
                    number = 0;
                    firstStep = true;
                    return;
                }
            } else if (number == 4) {
                step1.transform.Find("Ex").gameObject.SetActive(false);
                overallRotation.transform.Find("ShiftZ").gameObject.SetActive(true);
                if (cube.sz == -1) {
                    cube.sz = 0;
                } else if (cube.sz >= 1) {
                    overallRotation.transform.Find("ShiftZ").gameObject.SetActive(false);
                    cube.sz = -1;
                    direction = 0;
                    number = 0;
                    firstStep = true;
                    return;
                }
            } else if (number == 5) {
                step1.transform.Find("Ex").gameObject.SetActive(false);
                overallRotation.transform.Find("Z").gameObject.SetActive(true);
                if (cube.z == -1) {
                    cube.z = 0;
                } else if (cube.z >= 1) {
                    overallRotation.transform.Find("Z").gameObject.SetActive(false);
                    cube.z = -1;
                    direction = 0;
                    number = 0;
                    firstStep = true;
                    return;
                }
            }
        }
    }
    public void secondStepTest () {
        if (secondStep) {
            return;
        }
        step1.SetActive(false);
        step2.SetActive(true);
        if (direction == 0) {
            List<int> num1 = new List<int>();
            num1.Add(4);
            List<int> num2 = new List<int>();
            num2.Add(1);
            int count1 = 0, count2 = 0, count3 = 0, count4 = 0;
            for (int i = 0; i < frontState(num1).Length; i++) {
                if (frontState(num1)[i] == frontState(num2)[i] &&
                rightState(num1)[i] == rightState(num2)[i] &&
                leftState(num1)[i] == leftState(num2)[i] &&
                backState(num1)[i] == backState(num2)[i]) {
                    count1++;
                }
                if (frontState(num1)[i] == rightState(num2)[i] &&
                    rightState(num1)[i] == frontState(num2)[i] &&
                    leftState(num1)[i] == leftState(num2)[i] &&
                    backState(num1)[i] == backState(num2)[i]) {
                    count2++;
                }
                if (frontState(num1)[i] == frontState(num2)[i] &&
                    rightState(num1)[i] == leftState(num2)[i] &&
                    leftState(num1)[i] == rightState(num2)[i] &&
                    backState(num1)[i] == backState(num2)[i]) {
                    count3++;
                } 
            }
            if (count1 == frontState(num1).Length) {
                secondStep = true;
                formulaNum = 0;
                direction = 0;
                number = 0;
                return;
            } else if(count2 == frontState(num1).Length) {
                number = 20;
                direction = 1;
            } else if (count3 == frontState(num1).Length) {
                number = 10;
                direction = 1;
            } else {
                if (y4 < 4) {
                    number = 0;
                    direction = 1;
                    y4++;
                } else {
                    number = 1;
                    direction = 1;
                    y4 = 0;
                }
            }
        }
        if(direction == 1) {
            if(number == 0) {
                if (cube.u == -1) {
                    overallRotation.transform.Find("U").gameObject.SetActive(true);
                    cube.u = 0;
                } else if (cube.u >= 1) {
                    overallRotation.transform.Find("U").gameObject.SetActive(false);
                    direction = 0;
                    number = 0;
                    cube.u = -1;
                }
            } else if (number == 1) {
                if (cube.y == -1) {
                    overallRotation.transform.Find("Y").gameObject.SetActive(true);
                    cube.y = 0;
                } else if (cube.y >= 1) {
                    overallRotation.transform.Find("Y").gameObject.SetActive(false);
                    direction = 0;
                    number = 0;
                    cube.y = -1;
                }
            }
            // 공식 1
            else if (number == 10) {
                step2.transform.Find("Step2_Situation2").gameObject.SetActive(true);
                if (formulaNum == 0) {
                    if (cubeRotate(' ', 'r', 2)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate('s', 'u', 2)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate(' ', 'r', 2)) {
                        formulaNum = 3;
                    }
                }
                if (formulaNum == 3) {
                    if (cubeRotate('s', 'u', 2)) {
                        formulaNum = 4;
                    }
                }
                if (formulaNum == 4) {
                    if (cubeRotate(' ', 'r', 2)) {
                        formulaNum = 0;
                        direction = 0;
                        number = 0;
                        step2.transform.Find("Step2_Situation2").gameObject.SetActive(false);
                    }
                }
            }
            // 공식 2
            else if (number == 20) {
                step2.transform.Find("Step2_Situation1").gameObject.SetActive(true);
                if (formulaNum == 0) {
                    if (cubeRotate(' ', 'r', 2)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate('s', 'u', 1)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate(' ', 'r', 2)) {
                        formulaNum = 3;
                    }
                }
                if (formulaNum == 3) {
                    if (cubeRotate(' ', 'u', 1)) {
                        formulaNum = 4;
                    }
                }
                if (formulaNum == 4) {
                    if (cubeRotate(' ', 'r', 2)) {
                        number = 0;
                        formulaNum = 0;
                        direction = 0;
                        step2.transform.Find("Step2_Situation1").gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    public void thirdStepTest () {
        if (thirdStep) {
            return;
        }
        step2.SetActive(false);
        step3.SetActive(true);
        if (!ready) {
            direction = -1;
            ready = true;
        }
        if (direction == -1) {
            if (cube.x == -1) {
                overallRotation.transform.Find("X2").gameObject.SetActive(true);
                cube.x = 0;
            } else if (cube.x >= 2) {
                overallRotation.transform.Find("X2").gameObject.SetActive(false);
                direction = 0;
                number = 0;
                cube.x = -1;
            }
        } else if (direction == 0) {
            List<int> num = new List<int>();
            num.Add(6); num.Add(7); num.Add(8);
            int count = 0;
            if (test(frontState(num), num.Count)) {
                count++;
            }
            if (test(backState(num), num.Count)) {
                count++;
            }
            if (test(leftState(num), num.Count)) {
                count++;
            }
            if (test(rightState(num), num.Count)) {
                count++;
            }
            if (count == 4) {
                thirdStep = true;
                direction = 0;
                number = 0;
                y4 = 0;
                return;
            }
            List<int> num1 = new List<int>(); num1.Add(4); // 중간 큐브조각
            List<int> num2 = new List<int>(); num2.Add(2); // 오른쪽 위 큐브조각
            List<int> num3 = new List<int>(); num3.Add(0); // 왼쪽 위 큐브조각
            List<int> num4 = new List<int>(); num4.Add(8); // 오른쪽 아래 큐브조각
            List<int> num5 = new List<int>(); num5.Add(6); // 왼쪽 아래 큐브조각

            int count1 = 0, count2 = 0, count3 = 0, count4 = 0, count5 = 0, count6 = 0;
            for (int i = 0; i < frontState(num1).Length; i++) {
                if ((frontState(num1)[i] == frontState(num2)[i]) &&
                    (rightState(num1)[i] == upState(num4)[i]) &&
                    (downState(num1)[i] == rightState(num3)[i])) {
                    count1++;
                }
                if ((frontState(num1)[i] == upState(num4)[i]) &&
                    (rightState(num1)[i] == rightState(num3)[i]) &&
                    (downState(num1)[i] == frontState(num2)[i])) {
                    count2++;
                }
                if ((frontState(num1)[i] == rightState(num3)[i]) &&
                    (rightState(num1)[i] == frontState(num2)[i]) &&
                    (downState(num1)[i] == upState(num4)[i])) {
                    count3++;
                }
                if ((frontState(num1)[i] == frontState(num4)[i]) &&
                   (rightState(num1)[i] == rightState(num5)[i]) &&
                   (downState(num1)[i] == downState(num2)[i])) {
                    count4++;
                }
                if (downState(num1)[i] == frontState(num4)[i] ||
                    downState(num1)[i] == rightState(num5)[i]) {
                    count5++;
                }
                if (downState(num1)[i] == downState(num2)[i] &&
                    frontState(num1)[i] != frontState(num4)[i] ||
                    rightState(num1)[i] != rightState(num5)[i]) {
                    count6++;
                }
                /*
                if ((frontState(num1)[i] == rightState(num5)[i]) &&
                    (rightState(num1)[i] == frontState(num4)[i] || rightState(num1)[i] == downState(num2)[i]) &&
                    (downState(num1)[i] == frontState(num4)[i] || downState(num1)[i] == downState(num2)[i])) {
                    count5++;
                }
                if ((frontState(num1)[i] == downState(num2)[i]) &&
                    (rightState(num1)[i] == frontState(num4)[i] || rightState(num1)[i] == rightState(num5)[i]) &&
                    (downState(num1)[i] == frontState(num4)[i] || downState(num1)[i] == rightState(num5)[i])) {
                    count6++;
                }*/
            }
            if (count1 == frontState(num1).Length) {
                direction = 1;
                number = 10;
                y4 = 0;
            } else if (count2 == frontState(num1).Length) {
                direction = 1;
                number = 20;
                y4 = 0;
            } else if (count3 == frontState(num1).Length) {
                direction = 1;
                number = 30;
                y4 = 0;
            } else if (count4 == frontState(num1).Length) {
                direction = 1;
                number = -2;
                y4 = 0;
            } else if (count5 == frontState(num1).Length || count6 == frontState(num1).Length) {
                direction = 1;
                number = 10;
                y4 = 0;
            } else {
                y4++;
                direction = 1;
                number = -1;
                if (y4 >= 5) {
                    number = -2;
                    y4 = 0;
                }
            }
        } else if (direction == 1) {
            if (number == -2) {
                if (cube.y == -1) {
                    overallRotation.transform.Find("Y").gameObject.SetActive(true);
                    cube.y = 0;
                } else if (cube.y >= 1) {
                    overallRotation.transform.Find("Y").gameObject.SetActive(false);
                    direction = 0;
                    number = 0;
                    cube.y = -1;
                }
            }
            if (number == -1) {
                if (cube.u == -1) {
                    overallRotation.transform.Find("U").gameObject.SetActive(true);
                    cube.u = 0;
                } else if (cube.u >= 1) {
                    overallRotation.transform.Find("U").gameObject.SetActive(false);
                    direction = 0;
                    number = 0;
                    cube.u = -1;
                }
            }
            if (number == 10) {
                if (formulaNum == 0) {
                    step3.transform.Find("Step3_Situation1").gameObject.SetActive(true);
                    if (cubeRotate(' ', 'r', 1)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate(' ', 'u', 1)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate('s', 'r', 1)) {
                        formulaNum = 3;
                        step3.transform.Find("Step3_Situation1").gameObject.SetActive(false);
                    }
                }
                if (formulaNum == 3) {
                    if (cube.y == -1) {
                        overallRotation.transform.Find("Y").gameObject.SetActive(true);
                        cube.y = 0;
                    } else if (cube.y >= 1) {
                        cube.y = -1;
                        direction = 0;
                        number = 0;
                        formulaNum = 0;
                        overallRotation.transform.Find("Y").gameObject.SetActive(false);

                    }
                }
            } else if (number == 20) {
                if (formulaNum == 0) {
                    step3.transform.Find("Step3_Situation2").gameObject.SetActive(true);
                    if (cubeRotate('s', 'r', 1)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate(' ', 'f', 1)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate(' ', 'r', 1)) {
                        formulaNum = 3;
                    }
                }
                if (formulaNum == 3) {
                    if (cubeRotate('s', 'f', 1)) {
                        formulaNum = 4;
                        step3.transform.Find("Step3_Situation2").gameObject.SetActive(false);
                    }
                }
                if (formulaNum == 4) {
                    if (cube.y == -1) {
                        overallRotation.transform.Find("Y").gameObject.SetActive(true);
                        cube.y = 0;
                    } else if (cube.y >= 1) {
                        cube.y = -1;
                        direction = 0;
                        number = 0;
                        formulaNum = 0;
                        overallRotation.transform.Find("Y").gameObject.SetActive(false);
                    }
                }
            } else if (number == 30) {
                if (formulaNum == 0) {
                    step3.transform.Find("Step3_Situation3").gameObject.SetActive(true);
                    if (cubeRotate(' ', 'r', 1)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate(' ', 'u', 2)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate('s', 'r', 1)) {
                        formulaNum = 3;
                    }
                }
                if (formulaNum == 3) {
                    if (cubeRotate('s', 'u', 1)) {
                        formulaNum = 4;
                    }
                }
                if (formulaNum == 4) {
                    if (cubeRotate(' ', 'r', 1)) {
                        formulaNum = 5;
                    }
                }

                if (formulaNum == 5) {
                    if (cubeRotate(' ', 'u', 1)) {
                        formulaNum = 6;
                    }
                }
                if (formulaNum == 6) {
                    if (cubeRotate('s', 'r', 1)) {
                        formulaNum = 7;
                        step3.transform.Find("Step3_Situation3").gameObject.SetActive(false);
                    }
                }
                if (formulaNum == 7) {
                    if (cube.y == -1) {
                        overallRotation.transform.Find("Y").gameObject.SetActive(true);
                        cube.y = 0;
                    } else if (cube.y >= 1) {
                        cube.y = -1;
                        direction = 0;
                        number = 0;
                        formulaNum = 0;
                        overallRotation.transform.Find("Y").gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    public void fourthStepTest () {
        if (fourthStep) {
            return;
        }
        step3.SetActive(false);
        step4.SetActive(true);
        if (direction == 0) {
            List<int> num = new List<int>();
            num.Add(3); num.Add(4); num.Add(5);
            int count = 0;
            if (test(frontState(num), num.Count)) {
                count++;
            }
            if (test(backState(num), num.Count)) {
                count++;
            }
            if (test(leftState(num), num.Count)) {
                count++;
            }
            if (test(rightState(num), num.Count)) {
                count++;
            }
            if (count == 4) {
                fourthStep = true;
                direction = 0;
                number = 0;
                y4 = 0;
                formulaNum = 0;
                return;
            }
            List<int> num1 = new List<int>(); num1.Add(4); // 큐브조각 중간
            List<int> num2 = new List<int>(); num2.Add(7); // 큐브조각 중간 아래
            List<int> num3 = new List<int>(); num3.Add(5); // 큐브조각 중간 오른쪽
            List<int> num4 = new List<int>(); num4.Add(1); // 큐브조각 중간 위
            List<int> num5 = new List<int>(); num5.Add(3); // 큐브조각 중간 왼쪽
            int count1 = 0, count2 = 0, count3 = 0, count4 = 0, count5 = 0, count6 = 0, count7 = 0, count8 = 0, count9 = 0;
            for (int i = 0; i < frontState(num1).Length; i++) {
                if (frontState(num1)[i] == frontState(num4)[i]) {
                    if (upState(num2)[i] == rightState(num1)[i]) {
                        count1++;
                    }
                    if (upState(num2)[i] == leftState(num1)[i]) {
                        count2++;
                    }
                }
                if ((frontState(num1)[i] == frontState(num3)[i]) &&
                    (rightState(num1)[i] == rightState(num5)[i])) {
                    count3++;
                }
                if ((frontState(num1)[i] == frontState(num5)[i]) &&
                    (leftState(num1)[i] == leftState(num3)[i])) {
                    count4++;
                }
                if ((frontState(num1)[i] == rightState(num5)[i]) &&
                    (rightState(num1)[i] == frontState(num3)[i])) {
                    count5++;
                }
                if ((frontState(num1)[i] == leftState(num3)[i]) &&
                    (leftState(num1)[i] == frontState(num5)[i])) {
                    count6++;
                }
                if (frontState(num1)[i] == rightState(num5)[i]) {
                    if (rightState(num1)[i] == frontState(num3)[i]) {
                        count7++;
                    }
                    if (leftState(num1)[i] == frontState(num3)[i]) {
                        count8++;
                    }
                }
                if (frontState(num1)[i] == frontState(num3)[i] &&
                    rightState(num5)[i] == leftState(num1)[i]) {
                    count9++;
                }
            }
            if (count1 == frontState(num1).Length) {
                number = 10;
                direction = 1;
                y4 = 0;
            } else if (count2 == frontState(num1).Length) {
                number = 20;
                direction = 1;
                y4 = 0;
            } else if (count3 == frontState(num1).Length && count4 == frontState(num1).Length) {
                number = 1;
                direction = 1;
                y4 = 0;
            } else if (count5 == frontState(num1).Length) {
                number = 10;
                direction = 1;
                y4 = 0;
            } else if (count6 == frontState(num1).Length) {
                number = 20;
                direction = 1;
                y4 = 0;
            } else if (count7 == frontState(num1).Length || count8 == frontState(num1).Length ||
                       count9 == frontState(num1).Length) {
                number = 10;
                direction = 1;
                y4 = 0;
            } else {
                number = 0;
                direction = 1;
                y4++;
                if (y4 >= 5) {
                    number = 1;
                    y4 = 0;
                }

            }
        } else if (direction == 1) {
            if (number == 0) {
                if (cube.u == -1) {
                    overallRotation.transform.Find("U").gameObject.SetActive(true);
                    cube.u = 0;
                } else if (cube.u >= 1) {
                    cube.u = -1;
                    direction = 0;
                    overallRotation.transform.Find("U").gameObject.SetActive(false);
                }
            }
            if (number == 1) {
                if (cube.y == -1) {
                    overallRotation.transform.Find("Y").gameObject.SetActive(true);
                    cube.y = 0;
                } else if (cube.y >= 1) {
                    cube.y = -1;
                    direction = 0;
                    number = 0;
                    overallRotation.transform.Find("Y").gameObject.SetActive(false);
                }
            }
            if (number == 10) {
                if (formulaNum == 0) {
                    step4.transform.Find("Step4_Situation1").gameObject.SetActive(true);
                    if (cubeRotate(' ', 'u', 1)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate(' ', 'r', 1)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate('s', 'u', 1)) {
                        formulaNum = 3;
                    }
                }
                if (formulaNum == 3) {
                    if (cubeRotate('s', 'r', 1)) {
                        formulaNum = 4;
                    }
                }
                if (formulaNum == 4) {
                    if (cubeRotate(' ', 'f', 1)) {
                        formulaNum = 5;
                    }
                }
                if (formulaNum == 5) {
                    if (cubeRotate('s', 'r', 1)) {
                        formulaNum = 6;
                    }
                }
                if (formulaNum == 6) {
                    if (cubeRotate('s', 'f', 1)) {
                        formulaNum = 7;
                    }
                }
                if (formulaNum == 7) {
                    if (cubeRotate(' ', 'r', 1)) {
                        formulaNum = 0;
                        number = 0;
                        direction = 0;
                        step4.transform.Find("Step4_Situation1").gameObject.SetActive(false);
                    }
                }
            }
            if (number == 20) {
                if (formulaNum == 0) {
                    step4.transform.Find("Step4_Situation2").gameObject.SetActive(true);
                    if (cubeRotate('s', 'u', 1)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate('s', 'l', 1)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate(' ', 'u', 1)) {
                        formulaNum = 3;
                    }
                }
                if (formulaNum == 3) {
                    if (cubeRotate(' ', 'l', 1)) {
                        formulaNum = 4;
                    }
                }
                if (formulaNum == 4) {
                    if (cubeRotate('s', 'f', 1)) {
                        formulaNum = 5;
                    }
                }
                if (formulaNum == 5) {
                    if (cubeRotate(' ', 'l', 1)) {
                        formulaNum = 6;
                    }
                }
                if (formulaNum == 6) {
                    if (cubeRotate(' ', 'f', 1)) {
                        formulaNum = 7;
                    }
                }
                if (formulaNum == 7) {
                    if (cubeRotate('s', 'l', 1)) {
                        formulaNum = 0;
                        number = 0;
                        direction = 0;
                        step4.transform.Find("Step4_Situation2").gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    public void fifthStepTest () {
        if (fifthStep) {
            return;
        }
        step4.SetActive(false);
        step5.SetActive(true);
        if (direction == 0) {
            List<int> num = new List<int>();
            num.Add(1); num.Add(3); num.Add(4); num.Add(5); num.Add(7);
            int count = 0;
            if (test(upState(num), num.Count)) {
                count++;
            }
            if (count == 1) {
                fifthStep = true;
                direction = 0;
                number = 0;
                y4 = 0;
                formulaNum = 0;
                return;
            }
            List<int> num1 = new List<int>(); num1.Add(3); num1.Add(4); num1.Add(5);
            List<int> num2 = new List<int>(); num2.Add(1); num2.Add(3); num2.Add(4);
            int count1 = 0, count2 = 0;
            if (test(upState(num1), num1.Count)) {
                count1++;
            }
            if (test(upState(num2), num2.Count)) {
                count2++;
            }
            if (count1 == 1) {
                number = 10;
                direction = 1;
                y4 = 0;
            } else if (count2 == 1) {
                number = 10;
                direction = 1;
                y4 = 0;
            } else {
                number = 0;
                direction = 1;
                y4++;
                if (y4 >= 5) {
                    number = 10;
                    y4 = 0;
                }
            }
        } else if (direction == 1) {
            if (number == 0) {
                overallRotation.transform.Find("U").gameObject.SetActive(true);
                if (cube.u == -1) {
                    cube.u = 0;
                } else if (cube.u >= 1) {
                    cube.u = -1;
                    direction = 0;
                    overallRotation.transform.Find("U").gameObject.SetActive(false);
                }
            }
            if (number == 10) {
                if (formulaNum == 0) {
                    step5.transform.Find("Step5_Situation1").gameObject.SetActive(true);
                    if (cubeRotate(' ', 'f', 1)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate(' ', 'r', 1)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate(' ', 'u', 1)) {
                        formulaNum = 3;
                    }
                }
                if (formulaNum == 3) {
                    if (cubeRotate('s', 'r', 1)) {
                        formulaNum = 4;
                    }
                }
                if (formulaNum == 4) {
                    if (cubeRotate('s', 'u', 1)) {
                        formulaNum = 5;
                    }
                }
                if (formulaNum == 5) {
                    if (cubeRotate('s', 'f', 1)) {
                        formulaNum = 0;
                        number = 0;
                        direction = 0;
                        step5.transform.Find("Step5_Situation1").gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    public void sixthStepTest () {
        if (sixthStep) {
            return;
        }
        step5.SetActive(false);
        step6.SetActive(true);
        if (direction == 0) {
            List<int> num = new List<int>();
            num.Add(0); num.Add(1); num.Add(2); num.Add(3); num.Add(4);
            num.Add(5); num.Add(6); num.Add(7); num.Add(8);
            if (test(upState(num), num.Count)) {
                sixthStep = true;
                direction = 0;
                number = 0;
                y4 = 0;
                formulaNum = 0;
                return;
            }
            List<int> num1 = new List<int>();
            num1.Add(0); num1.Add(1); num1.Add(3); num1.Add(4); num1.Add(5); num1.Add(7); num1.Add(8);
            List<int> num2 = new List<int>();
            num2.Add(0); num2.Add(1); num2.Add(3); num2.Add(4); num2.Add(5); num2.Add(6); num2.Add(7);
            List<int> num3 = new List<int>();
            num3.Add(1); num3.Add(2); num3.Add(3); num3.Add(4); num3.Add(5); num3.Add(7); num3.Add(8);
            List<int> num4 = new List<int>();
            num4.Add(1); num4.Add(3); num4.Add(4); num4.Add(5); num4.Add(7); num4.Add(8);
            List<int> num5 = new List<int>();
            num5.Add(0); num5.Add(1); num5.Add(3); num5.Add(4); num5.Add(5); num5.Add(7);
            List<int> num6 = new List<int>();
            num6.Add(1); num6.Add(3); num6.Add(4); num6.Add(5); num6.Add(7);
            List<int> num7 = new List<int>();
            num7.Add(1); num7.Add(3); num7.Add(4); num7.Add(5); num7.Add(6); num7.Add(7); num7.Add(8);
            List<int> num11 = new List<int>(); num11.Add(0);
            List<int> num12 = new List<int>(); num12.Add(2);
            List<int> num13 = new List<int>(); num13.Add(4);
            for (int i = 0; i < upState(num1).Length; i++) {
                if (upState(num1)[i] == num1.Count) {
                    if (upState(num13)[i] == frontState(num11)[i] &&
                        upState(num13)[i] == rightState(num12)[i]) {
                        direction = 1;
                        number = 10;
                        return;
                    } else {
                        direction = 1;
                        number = 2;
                        return;
                    }
                } else if (upState(num2)[i] == num2.Count) {
                    if (upState(num13)[i] == rightState(num11)[i] &&
                        upState(num13)[i] == rightState(num12)[i]) {
                        direction = 1;
                        number = 10;
                        return;
                    } else {
                        direction = 1;
                        number = 2;
                        return;
                    }
                } else if (upState(num3)[i] == num3.Count) {
                    if (upState(num13)[i] == frontState(num11)[i] &&
                        upState(num13)[i] == backState(num12)[i]) {
                        direction = 1;
                        number = 10;
                        return;
                    } else {
                        direction = 1;
                        number = 2;
                        return;
                    }
                } else if (upState(num7)[i] == num7.Count) {
                    number = 1;
                    direction = 1;
                    return;
                } else if (upState(num4)[i] == num4.Count) {
                    if (upState(num13)[i] == frontState(num11)[i] &&
                        upState(num13)[i] == leftState(num11)[i] &&
                        upState(num13)[i] == backState(num11)[i]) {
                        direction = 1;
                        number = 10;
                        return;
                    } else {
                        direction = 1;
                        number = 2;
                        return;
                    }
                } else if (upState(num5)[i] == num5.Count) {
                    if (upState(num13)[i] == frontState(num12)[i] &&
                        upState(num13)[i] == leftState(num12)[i] &&
                        upState(num13)[i] == rightState(num12)[i]) {
                        direction = 1;
                        number = 10;
                        return;
                    } else {
                        direction = 1;
                        number = 2;
                        return;
                    }
                } else if (upState(num6)[i] == num6.Count) {
                    if (frontState(num11)[i] == frontState(num12)[i] &&
                        frontState(num11)[i] == rightState(num12)[i] &&
                        frontState(num11)[i] == leftState(num11)[i]) {
                        direction = 1;
                        number = 10;
                        return;
                    }
                    if (frontState(num11)[i] == frontState(num12)[i] &&
                        frontState(num11)[i] == backState(num11)[i] &&
                        frontState(num11)[i] == backState(num12)[i]) {
                        direction = 1;
                        number = 10;
                        return;
                    }
                } else {
                    direction = 1;
                    number = 1;
                }
            }
        } else if (direction == 1) {
            if (number == 1) {
                if (cube.u == -1) {
                    overallRotation.transform.Find("U").gameObject.SetActive(true);
                    cube.u = 0;
                } else if (cube.u >= 1) {
                    cube.u = -1;
                    direction = 0;
                    number = 0;
                    overallRotation.transform.Find("U").gameObject.SetActive(false);
                }
            }
            if (number == 2) {
                if (cube.u == -1) {
                    overallRotation.transform.Find("U2").gameObject.SetActive(true);
                    cube.u = 0;
                } else if (cube.u >= 2) {
                    cube.u = -1;
                    direction = 0;
                    number = 0;
                    overallRotation.transform.Find("U2").gameObject.SetActive(false);
                }
            }
            if (number == 10) {
                if (formulaNum == 0) {
                    step6.transform.Find("Step6_Situation1").gameObject.SetActive(true);
                    if (cubeRotate(' ', 'f', 1)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate('s', 'r', 1)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate('s', 'f', 1)) {
                        formulaNum = 3;
                    }
                }
                if (formulaNum == 3) {
                    if (cubeRotate(' ', 'l', 1)) {
                        formulaNum = 4;
                    }
                }
                if (formulaNum == 4) {
                    if (cubeRotate(' ', 'f', 1)) {
                        formulaNum = 5;
                    }
                }
                if (formulaNum == 5) {
                    if (cubeRotate(' ', 'r', 1)) {
                        formulaNum = 6;
                    }
                }
                if (formulaNum == 6) {
                    if (cubeRotate('s', 'f', 1)) {
                        formulaNum = 7;
                    }
                }
                if (formulaNum == 7) {
                    if (cubeRotate('s', 'l', 1)) {
                        formulaNum = 0;
                        number = 0;
                        direction = 0;
                        step6.transform.Find("Step6_Situation1").gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    public void seventhSteptest () {
        if (seventhStep) {
            return;
        }
        step6.SetActive(false);
        step7.SetActive(true);
        if (direction == 0) {
            List<int> num = new List<int>();
            num.Add(0); num.Add(2); num.Add(4);
            if (test(frontState(num), num.Count) &&
                test(backState(num), num.Count) &&
                test(leftState(num), num.Count) &&
                test(rightState(num), num.Count)) {
                seventhStep = true;
                direction = 0;
                number = 0;
                y4 = 0;
                formulaNum = 0;
                return;
            }
            List<int> num1 = new List<int>(); num1.Add(0);
            List<int> num2 = new List<int>(); num2.Add(2);
            List<int> num3 = new List<int>(); num3.Add(4);
            int count1 = 0, count2 = 0;
            for (int i = 0; i < frontState(num1).Length; i++) {
                if (frontState(num1)[i] == frontState(num3)[i]) {
                    if (frontState(num1)[i] == rightState(num2)[i] &&
                        frontState(num2)[i] == backState(num1)[i] &&
                        leftState(num1)[i] == leftState(num2)[i] &&
                        backState(num2)[i] == rightState(num1)[i]) {
                        // 공식 1
                        count1++;
                    }
                    if (frontState(num1)[i] == backState(num2)[i] &&
                        frontState(num2)[i] == backState(num1)[i] &&
                        rightState(num1)[i] == leftState(num2)[i] &&
                        rightState(num2)[i] == leftState(num1)[i]) {
                        // 공식 2
                        count2++;
                    }
                }
            }
            if (count1 == frontState(num1).Length) {
                number = 10;
                direction = 1;
                y4 = 0;
            } else if (count2 == frontState(num1).Length) {
                number = 20;
                direction = 1;
                y4 = 0;
            } else {
                number = 1;
                direction = 1;
                y4++;
                if (y4 >= 5) {
                    number = 3;
                    direction = 1;
                    y4 = 0;
                }
            }
        } else if (direction == 1) {
            if (number == 1) {
                if (cube.u == -1) {
                    overallRotation.transform.Find("U").gameObject.SetActive(true);
                    cube.u = 0;
                } else if (cube.u >= 1) {
                    cube.u = -1;
                    direction = 0;
                    number = 0;
                    overallRotation.transform.Find("U").gameObject.SetActive(false);
                }
            }
            if (number == 2) {
                if (cube.sy == -1) {
                    overallRotation.transform.Find("ShiftY").gameObject.SetActive(true);
                    cube.sy = 0;
                } else if (cube.sy >= 1) {
                    cube.sy = -1;
                    direction = 0;
                    number = 0;
                    overallRotation.transform.Find("ShiftY").gameObject.SetActive(false);
                }
            }
            if (number == 3) {
                if (cube.y == -1) {
                    overallRotation.transform.Find("Y").gameObject.SetActive(true);
                    cube.y = 0;
                } else if (cube.y >= 1) {
                    cube.y = -1;
                    direction = 0;
                    number = 0;
                    overallRotation.transform.Find("Y").gameObject.SetActive(false);
                }
            }
            if (number == 10) {
                if (formulaNum == 0) {
                    step7.transform.Find("Step7_Situation1").gameObject.SetActive(true);
                    if (cubeRotate(' ', 'r', 1)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate(' ', 'u', 2)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate('s', 'r', 1)) {
                        formulaNum = 3;
                    }
                }
                if (formulaNum == 3) {
                    if (cubeRotate('s', 'u', 1)) {
                        formulaNum = 4;
                    }
                }
                if (formulaNum == 4) {
                    if (cubeRotate(' ', 'r', 1)) {
                        formulaNum = 5;
                    }
                }
                if (formulaNum == 5) {
                    if (cubeRotate(' ', 'u', 2)) {
                        formulaNum = 6;
                    }
                }
                if (formulaNum == 6) {
                    if (cubeRotate('s', 'l', 1)) {
                        formulaNum = 7;
                    }
                }
                if (formulaNum == 7) {
                    if (cubeRotate(' ', 'u', 1)) {
                        formulaNum = 8;
                    }
                }
                if (formulaNum == 8) {
                    if (cubeRotate('s', 'r', 1)) {
                        formulaNum = 9;
                    }
                }
                if (formulaNum == 9) {
                    if (cubeRotate('s', 'u', 1)) {
                        formulaNum = 10;
                    }
                }
                if (formulaNum == 10) {
                    if (cubeRotate(' ', 'l', 1)) {
                        formulaNum = 0;
                        number = 0;
                        direction = 0;
                        step7.transform.Find("Step7_Situation1").gameObject.SetActive(false);
                    }
                }
            }
            if (number == 20) {
                if (formulaNum == 0 || formulaNum == 11) {
                    step7.transform.Find("Step7_Situation2").gameObject.SetActive(true);
                    if (cubeRotate(' ', 'r', 1)) {
                        if (formulaNum > 10) {
                            formulaNum = 12;
                        } else {
                            formulaNum = 1;
                        }
                    }
                }
                if (formulaNum == 1 || formulaNum == 12) {
                    if (cubeRotate(' ', 'u', 2)) {
                        if (formulaNum > 10) {
                            formulaNum = 13;
                        } else {
                            formulaNum = 2;
                        }
                    }
                }
                if (formulaNum == 2 || formulaNum == 13) {
                    if (cubeRotate('s', 'r', 1)) {
                        if (formulaNum > 10) {
                            formulaNum = 14;
                        } else {
                            formulaNum = 3;
                        }
                    }
                }
                if (formulaNum == 3 || formulaNum == 14) {
                    if (cubeRotate('s', 'u', 1)) {
                        if (formulaNum > 10) {
                            formulaNum = 15;
                        } else {
                            formulaNum = 4;
                        }
                    }
                }
                if (formulaNum == 4 || formulaNum == 15) {
                    if (cubeRotate(' ', 'r', 1)) {
                        if (formulaNum > 10) {
                            formulaNum = 16;
                        } else {
                            formulaNum = 5;
                        }
                    }
                }
                if (formulaNum == 5 || formulaNum == 16) {
                    if (cubeRotate(' ', 'u', 2)) {
                        if (formulaNum > 10) {
                            formulaNum = 17;
                        } else {
                            formulaNum = 6;
                        }
                    }
                }
                if (formulaNum == 6 || formulaNum == 17) {
                    if (cubeRotate('s', 'l', 1)) {
                        if (formulaNum > 10) {
                            formulaNum = 18;
                        } else {
                            formulaNum = 7;
                        }
                    }
                }
                if (formulaNum == 7 || formulaNum == 18) {
                    if (cubeRotate(' ', 'u', 1)) {
                        if (formulaNum > 10) {
                            formulaNum = 19;
                        } else {
                            formulaNum = 8;
                        }
                    }
                }
                if (formulaNum == 8 || formulaNum == 19) {
                    if (cubeRotate('s', 'r', 1)) {
                        if (formulaNum > 10) {
                            formulaNum = 20;
                        } else {
                            formulaNum = 9;
                        }
                    }
                }
                if (formulaNum == 9 || formulaNum == 20) {
                    if (cubeRotate('s', 'u', 1)) {
                        if (formulaNum > 10) {
                            formulaNum = 21;
                        } else {
                            formulaNum = 10;
                        }
                    }
                }
                if (formulaNum == 10 || formulaNum == 21) {
                    if (cubeRotate(' ', 'l', 1)) {
                        if (formulaNum > 10) {
                            formulaNum = 0;
                            number = 0;
                            direction = 0;
                            step7.transform.Find("Step7_Situation2").gameObject.SetActive(false);
                        } else {
                            formulaNum = 11;
                        }
                    }
                }
            }
        }
    }
    public void eighthSteptest () {
        step7.SetActive(false);
        step8.SetActive(true);
        if (direction == 0) {
            List<int> num1 = new List<int>();
            num1.Add(1);
            List<int> num2 = new List<int>();
            num2.Add(4);
            int count0 = 0, count1 = 0, count2 = 0, count3 = 0, count4 = 0;
            for (int i = 0; i < frontState(num1).Length; i++) { 
                if (frontState(num1)[i] == frontState(num2)[i] &&
                    backState(num1)[i] == backState(num2)[i] &&
                    rightState(num1)[i] == rightState(num2)[i] &&
                    leftState(num1)[i] == leftState(num2)[i]) {
                    count0++;
                }
                if (frontState(num1)[i] == backState(num2)[i] &&
                    backState(num1)[i] == rightState(num2)[i] &&
                    rightState(num1)[i] == frontState(num2)[i]) {
                    // 공식 1번
                    count1++;
                }
                if (frontState(num1)[i] == rightState(num2)[i] &&
                    backState(num1)[i] == frontState(num2)[i] &&
                    rightState(num1)[i] == backState(num2)[i]) {
                    // 공식 1번
                    count2++;
                }
                if (frontState(num1)[i] == backState(num2)[i] &&
                    backState(num1)[i] == frontState(num2)[i] &&
                    rightState(num1)[i] == leftState(num2)[i] &&
                    leftState(num1)[i] == rightState(num2)[i]) {
                    // 공식 1번, y 1번
                    count3++;
                }
                if (frontState(num1)[i] == rightState(num2)[i] &&
                    backState(num1)[i] == leftState(num2)[i] &&
                    rightState(num1)[i] == frontState(num2)[i] &&
                    leftState(num1)[i] == backState(num2)[i]) {
                    // 공식 1번, y' 1번
                    count4++;
                }
            }
            if (count0 == frontState(num1).Length) {
                formulaNum = 0;
                direction = 0;
                number = 0;
                allObjectSetFalse();
                cubeClear.setClear(true);
            } else if (count1 == frontState(num1).Length || count2 == frontState(num1).Length) {
                direction = 1;
                number = 1;
            } else if (count3 == frontState(num1).Length) {
                direction = 1;
                number = 2;
            } else if (count4 == frontState(num1).Length) {
                direction = 1;
                number = 3;
            } else {
                number = -1;
                direction = 1;
            }
        } else if (direction == 1) {
            if(number == -1) {
                if (cube.y == -1) {
                    overallRotation.transform.Find("Y").gameObject.SetActive(true);
                    cube.y = 0;
                } else if (cube.y >= 1) {
                    cube.y = -1;
                    formulaNum = 0;
                    direction = 0;
                    number = 0;
                    overallRotation.transform.Find("Y").gameObject.SetActive(false);
                }
            } else if (number == 1 || number == 2 || number == 3) {
                if (formulaNum == 0) {
                    step8.transform.Find("Step8_Situation").gameObject.SetActive(true);
                    if (cubeRotate(' ', 'r', 2)) {
                        formulaNum = 1;
                    }
                }
                if (formulaNum == 1) {
                    if (cubeRotate(' ', 'u', 1)) {
                        formulaNum = 2;
                    }
                }
                if (formulaNum == 2) {
                    if (cubeRotate(' ', 'f', 1)) {
                        formulaNum = 3;
                    }
                }
                if (formulaNum == 3) {
                    if (cubeRotate('s', 'b', 1)) {
                        formulaNum = 4;
                    }
                }
                if (formulaNum == 4) {
                    if (cubeRotate(' ', 'r', 2)) {
                        formulaNum = 5;
                    }
                }
                if (formulaNum == 5) {
                    if (cubeRotate('s', 'f', 1)) {
                        formulaNum = 6;
                    }
                }
                if (formulaNum == 6) {
                    if (cubeRotate(' ', 'b', 1)) {
                        formulaNum = 7;
                    }
                }
                if (formulaNum == 7) {
                    if (cubeRotate(' ', 'u', 1)) {
                        formulaNum = 8;
                    }
                }
                if (formulaNum == 8) {
                    if (cubeRotate(' ', 'r', 2)) {
                        formulaNum = 9;
                        step8.transform.Find("Step8_Situation").gameObject.SetActive(false);
                    }
                }
                if (formulaNum == 9) {
                    if (number == 1) {
                        formulaNum = 0;
                        direction = 0;
                        number = 0;
                    }
                    if (number == 2) {
                        if (cube.y == -1) {
                            overallRotation.transform.Find("Y").gameObject.SetActive(true);
                            cube.y = 0;
                        } else if (cube.y >= 1) {
                            cube.y = -1;
                            formulaNum = 0;
                            direction = 0;
                            number = 0;
                            overallRotation.transform.Find("Y").gameObject.SetActive(false);
                        }
                    }
                    if (number == 3) {
                        if (cube.sy == -1) {
                            overallRotation.transform.Find("ShiftY").gameObject.SetActive(true);
                            cube.sy = 0;
                        } else if (cube.sy >= 1) {
                            cube.sy = -1;
                            formulaNum = 0;
                            direction = 0;
                            number = 0;
                            overallRotation.transform.Find("ShiftY").gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
    private bool cubeRotate (char c1, char c2, int num) {
        switch(c2) {
            case 'f':
                if (c1 == 's') {
                    if (cube.sf == -1) {
                        cube.sf = 0;
                    } else if (cube.sf >= num) {
                        cube.sf = -1;
                        return true;
                    }
                } else {
                    if (cube.f == -1) {
                        cube.f = 0;
                    } else if (cube.f >= num) {
                        cube.f = -1;
                        return true;
                    }
                }
                break;
            case 'b':
                if (c1 == 's') {
                    if (cube.sb == -1) {
                        cube.sb = 0;
                    } else if (cube.sb >= num) {
                        cube.sb = -1;
                        return true;
                    }
                } else {
                    if (cube.b == -1) {
                        cube.b = 0;
                    } else if (cube.b >= num) {
                        cube.b = -1;
                        return true;
                    }
                }
                break;
            case 'u':
                if (c1 == 's') {
                    if (cube.su == -1) {
                        cube.su = 0;
                    } else if (cube.su >= num) {
                        cube.su = -1;
                        return true;
                    }
                } else {
                    if (cube.u == -1) {
                        cube.u = 0;
                    } else if (cube.u >= num) {
                        cube.u = -1;
                        return true;
                    }
                }
                break;
            case 'd':
                if (c1 == 's') {
                    if (cube.sd == -1) {
                        cube.sd = 0;
                    } else if (cube.sd >= num) {
                        cube.sd = -1;
                        return true;
                    }
                } else {
                    if (cube.d == -1) {
                        cube.d = 0;
                    } else if (cube.d >= num) {
                        cube.d = -1;
                        return true;
                    }
                }
                break;
            case 'r':
                if (c1 == 's') {
                    if (cube.sr == -1) {
                        cube.sr = 0;
                    } else if (cube.sr >= num) {
                        cube.sr = -1;
                        return true;
                    }
                } else {
                    if (cube.r == -1) {
                        cube.r = 0;
                    } else if (cube.r >= num) {
                        cube.r = -1;
                        return true;
                    }
                }
                break;
            case 'l':
                if (c1 == 's') {
                    if (cube.sl == -1) {
                        cube.sl = 0;
                    } else if (cube.sl >= num) {
                        cube.sl = -1;
                        return true;
                    }
                } else {
                    if (cube.l == -1) {
                        cube.l = 0;
                    } else if (cube.l >= num) {
                        cube.l = -1;
                        return true;
                    }
                }
                break;
            case 'x':
                if (c1 == 's') {
                    if (cube.sx == -1) {
                        cube.sx = 0;
                    } else if (cube.sx >= num) {
                        cube.sx = -1;
                        return true;
                    }
                } else {
                    if (cube.x == -1) {
                        cube.x = 0;
                    } else if (cube.x >= num) {
                        cube.x = -1;
                        return true;
                    }
                }
                break;
            case 'y':
                if (c1 == 's') {
                    if (cube.sy == -1) {
                        cube.sy = 0;
                    } else if (cube.sy >= num) {
                        cube.sy = -1;
                        return true;
                    }
                } else {
                    if (cube.y == -1) {
                        cube.y = 0;
                    } else if (cube.y >= num) {
                        cube.y = -1;
                        return true;
                    }
                }
                break;
            case 'z':
                if (c1 == 's') {
                    if (cube.sz == -1) {
                        cube.sz = 0;
                    } else if (cube.sz >= num) {
                        cube.sz = -1;
                        return true;
                    }
                } else {
                    if (cube.z == -1) {
                        cube.z = 0;
                    } else if (cube.z >= num) {
                        cube.z = -1;
                        return true;
                    }
                }
                break;
        }
        return false;
    }
    public void allObjectSetFalse() {
        firstStep = false;
        secondStep = false;
        thirdStep = false;
        fourthStep = false;
        fifthStep = false;
        sixthStep = false;
        seventhStep = false;
        eighthStep = false;
        step1.SetActive(true);
        step2.SetActive(false);
        step3.SetActive(false);
        step4.SetActive(false);
        step5.SetActive(false);
        step6.SetActive(false);
        step7.SetActive(false);
        step8.SetActive(false);
        formulaMode.SetActive(false);
    }
    private void allSetZero() {
        cube.f = 0;
        cube.b = 0;
        cube.u = 0;
        cube.d = 0;
        cube.l = 0;
        cube.r = 0;
        cube.x = 0;
        cube.y = 0;
        cube.z = 0;
        cube.sf = 0;
        cube.sb = 0;
        cube.su = 0;
        cube.sd = 0;
        cube.sl = 0;
        cube.sr = 0;
        cube.sx = 0;
        cube.sy = 0;
        cube.sz = 0;
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
    public bool test (int[] count, int num) {
        for (int i = 0; i < count.Length; i++) {
            if (count[i] == num) {
                return true;
            }
        }
        return false;
    }
    private void allSetFalse () {
        cube.f = -1;
        cube.b = -1;
        cube.l = -1;
        cube.r = -1;
        cube.u = -1;
        cube.d = -1;
        cube.x = -1;
        cube.y = -1;
        cube.z = -1;
        cube.sf = -1;
        cube.sb = -1;
        cube.sl = -1;
        cube.sr = -1;
        cube.su = -1;
        cube.sd = -1;
        cube.sx = -1;
        cube.sy = -1;
        cube.sz = -1;
    }
}