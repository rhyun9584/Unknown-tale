using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public static ClueManager inst;

    public GameObject[] clues;
    public GameObject obtainUI, obtainScript, obtainPopUp;

    private void Awake()
    {
        inst = this;
    }

    /// <summary>
    /// 단서 획득
    /// 1. clue UI에 획득 정보 전달
    /// 2. 획득하는 화면 표시
    /// </summary>
    /// <param name="Clue"></param>
    public void ObtainClue(ClueBase clue)
    {

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

        obtainUI.SetActive(false);
        obtainScript.SetActive(false);
        obtainPopUp.SetActive(false);

    }
}