using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : MonoBehaviour
{
    public static ClueManager inst;

    public static int clueCount = 4;
    
    public GameObject obtainUI, obtainScript, obtainPopUp; // 스크립트로 매핑
    [HideInInspector]
    public bool[] isObtain = new bool[clueCount];

    //clue 획득 시 뜨는 획득 스크립트
    private string[] obtainSciptContent = new string[clueCount];

    private int clueNum;

    private void Awake()
    {
        inst = this;

        obtainUI = GameObject.Find("ObtainUI").gameObject;
        obtainScript = GameObject.Find("Script").gameObject;
        obtainPopUp = GameObject.Find("Popup").gameObject;
    
        obtainUI.SetActive(false);
        obtainScript.SetActive(false);
        obtainPopUp.SetActive(false);

        for(int i = 0; i < clueCount; i++)
        {
            isObtain[i] = false;
        }

        obtainSciptContent[0] = "";
        obtainSciptContent[1] = "";
        obtainSciptContent[2] = "( 역시! 이거라면, 내가 별로 움직이지 않았다는 걸 증명해줄 수 있겠어! )";
        obtainSciptContent[3] = "( 없지! )";

        // 트리거 없는 증거들을 시작부터 획득
        //ObtainClue(0, "selfie"); // 셀카
        //ObtainClue(1, "mud shoes"); // 진흙이 묻은 신발
    }


    /// <summary>
    /// 단서 획득
    /// 1. clue UI에 획득 정보 전달
    /// 2. 획득하는 화면 표시
    /// </summary>
    /// <param name="Clue"></param>
    public void ObtainClue(int clueNumber, string clueName)
    {
        ClueUI.inst.clueSlots[clueNumber].GetComponent<ClueSlot>().OpenButton(clueName);

        clueNum = clueNumber; // OpenObtainUI에 clue number 정보를 넘기기 위한 값
        StartCoroutine(OpenObtainUI());
    }
    
    IEnumerator OpenObtainUI()
    {
        bool next = true;

        obtainUI.SetActive(true);

        for (int i = 0; i < 3;)
        {
            if (next)
            {
                switch (i)
                {
                    case 1:
                        obtainScript.GetComponentInChildren<Text>().text = obtainSciptContent[clueNum];
                        obtainScript.SetActive(true);
                        break;
                    case 2:
                        obtainPopUp.SetActive(true);
                        break;
                }

                next = false;
            }
            else if (!next && Input.GetMouseButtonUp(0))
            {
                next = true;
                i++;
            }

            yield return null;
        }

        isObtain[clueNum] = true;

        obtainUI.SetActive(false);
        obtainScript.SetActive(false);
        obtainPopUp.SetActive(false);
    }
}