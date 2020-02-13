using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public static ClueManager inst;

    public static int clueCount = 4;
    
    public GameObject obtainUI, obtainScript, obtainPopUp; // 스크립트로 매핑
    [HideInInspector]
    public bool[] isObtain = new bool[clueCount];

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