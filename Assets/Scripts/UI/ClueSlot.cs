using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueSlot : MonoBehaviour
{
    public int clueNumber;
    
    private string clueName;
    private Image clueImage;
    private bool isObtain = false;

    private void Awake()
    {
        clueImage = GetComponentInChildren<Image>();
    }

    /// <summary>
    /// 증거 획득시 버튼 활성화
    /// </summary>
    public void OpenButton(string name)
    {
        // 임시로 컬러 변경으로 활성화 표현, 이후 이미지 교체로 변경
        clueImage.color = new Color((float)0.5, (float)0.5, (float)0.5);

        clueName = name;

        isObtain = true;
    }

    public void OpenDetail()
    {
        if (isObtain)
        {
            ClueUI.inst.OpenDetailUI(clueNumber, clueName);
        }
    }
}
