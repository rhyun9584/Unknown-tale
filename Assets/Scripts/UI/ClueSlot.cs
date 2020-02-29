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
        Debug.Log("clue" + clueNumber + "open");
        clue = Resources.Load<Clue>("Clue/" + Code);

        clueImage.sprite = clue.clueImage;

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
