using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : MonoBehaviour
{
    public static ClueManager inst;

    public static int clueCount = 4;
    
    public GameObject obtainUI, obtainScript, obtainPopUp, obtainImage; // 스크립트로 매핑
    public Clue[] clue= new Clue[clueCount]; // 직접 매핑
    [HideInInspector]
    public bool[] isObtain = new bool[clueCount];

    //clue 획득 시 뜨는 획득 스크립트
    private string[,] obtainSciptContent = new string[clueCount, 3];

    private int clueNum;

    private void Awake()
    {
        inst = this;

        obtainUI = GameObject.Find("ObtainUI").gameObject;
        obtainScript = GameObject.Find("Script").gameObject;
        obtainPopUp = GameObject.Find("Popup").gameObject;
        obtainImage = GameObject.Find("ObtainUI/Image").gameObject;
    
        obtainUI.SetActive(false);
        obtainScript.SetActive(false);
        obtainPopUp.SetActive(false);

        for(int i = 0; i < clueCount; i++)
        {
            isObtain[i] = false;
            for (int j = 0; j < clue[i].obtainScript.Length; j++)
            {
                obtainSciptContent[i, j] = clue[i].obtainScript[j];
            }
        }
        /*
        obtainSciptContent[0] = "";
        obtainSciptContent[1] = "";
        obtainSciptContent[2] = "( 역시! 이거라면, 내가 별로 움직이지 않았다는 걸 증명해줄 수 있겠어! )";
        obtainSciptContent[3] = "( 없지! )";
        */
    }

    private void Start()
    {
        // 획득 트리거 없는 증거들을 시작부터 획득
        ClueUI.inst.clueSlots[0].GetComponent<ClueSlot>().OpenButton("000"); // 셀카
        ClueUI.inst.clueSlots[1].GetComponent<ClueSlot>().OpenButton("011"); // 진흙이 묻은 신발
    }


    /// <summary>
    /// 단서 획득
    /// 1. clue UI에 획득 정보 전달
    /// 2. 획득하는 화면 표시
    /// </summary>
    /// <param name="Clue"></param>
    public void ObtainClue(int clueNumber, string clueCode)
    {
        ClueUI.inst.clueSlots[clueNumber].GetComponent<ClueSlot>().OpenButton(clueCode);

        clueNum = clueNumber; // OpenObtainUI에 clue number 정보를 넘기기 위한 값
        StartCoroutine(OpenObtainUI());
    }
    
    IEnumerator OpenObtainUI()
    {
        bool next = true;

        obtainUI.SetActive(true);
        obtainImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/clue/obtain" + clueNum.ToString());

        for (int i = 0; i < 3;)
        {
            if (next)
            {
                switch (i)
                {
                    case 1:
                        obtainScript.GetComponentInChildren<Text>().text = obtainSciptContent[clueNum, 0]; 
                        /*
                        여러개의 스크립트도 띄울수 있도록 수정 필요 &&&&&&
                        */
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