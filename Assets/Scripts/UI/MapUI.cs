using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public static MapUI inst;

    public GameObject mapUI;
    public GameObject exitButton, moveButton, background;

    // 상세히 창 이전의 정보 창 -> simple
    // 상세히 창                -> detail
    public GameObject simpleUI, simpleName, simpleExplain, simpleImage; 

    private Text simpleNameText, simpleExplainText;
    private Sprite simpleImageSprite;

    private bool isActive = false;
    private bool isSimpleActive = false;

    void Awake()
    {
        if (inst == null)
            inst = this;
    }

    private void Start()
    {
        simpleNameText = simpleName.GetComponent<Text>();
        simpleExplainText = simpleExplain.GetComponent<Text>();
        simpleImageSprite = simpleImage.GetComponent<Sprite>();
    }

    public void OpenMapUI()
    {
        if (!isActive)
        {
            isActive = true;

            PhoneUI.inst.HideMain();
            PhoneUI.inst.HideBackground();
            mapUI.SetActive(isActive);

            GameManager.inst.ChangeState(State.Map);
        }
    }

    public void CloseMapUI()
    {
        if (isActive)
        {
            isActive = false;

            PhoneUI.inst.ShowMain();
            PhoneUI.inst.ShowBackground();
            mapUI.SetActive(isActive);

            GameManager.inst.ChangeState(State.Phone);
        }
    }

    public void OnSimpleUI(LocationCode locationCode)
    {
        if (!isSimpleActive)
        {
            isSimpleActive = true;

            simpleNameText.text = locationCode.ToString();
            //simpleExplainText.text = ;
            //simpleImageSprite = Resources.Load("");

            simpleUI.SetActive(isSimpleActive);
        }
    }

    public void OffSimpleUI()
    {
        if (isSimpleActive)
        {
            isSimpleActive = false;

            simpleUI.SetActive(isSimpleActive);
        }
    }

    /// <summary>
    /// Map UI 내부의 Exit Button이 어떤 동작을 할 것인지 결정
    /// 가장 상단의 창을 닫음
    /// </summary>
    public void ExitButton()
    {
        if (isSimpleActive)
        {
            OffSimpleUI();
        }
        else if (isActive)
        {
            CloseMapUI();
        }
    }
}
