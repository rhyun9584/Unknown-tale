using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueSlot : MonoBehaviour
{
    public int clueNumber;
    
    private Image clueImage;
    private bool isObtain = false;

    private Clue clue;

    private void Awake()
    {
        clueImage = GetComponentInChildren<Image>();
    }

    /// <summary>
    /// 증거 획득시 버튼 활성화
    /// </summary>
    public void OpenButton(string Code)
    {
        // 임시로 컬러 변경으로 활성화 표현, 이후 이미지 교체로 변경
        clueImage.sprite = Resources.Load<Sprite>("UI/clue/" + clueNumber.ToString());
        Debug.Log("clue" + clueNumber + "open");
        /*
         위의 것도 스크립터블 오브젝트로 담기게 변경
        */
        clue = Resources.Load<Clue>("Clue/" + Code);

        isObtain = true;
    }

    public void OpenDetail()
    {
        if (isObtain)
        {
            ClueUI.inst.OpenDetailUI(clue);
        }
    }
}
