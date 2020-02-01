using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneUI : MonoBehaviour
{
    public static PhoneUI inst;

    public GameObject phoneShowButton;
    public GameObject phone, main, background, blur;

    private bool isActive = false;
    private bool isMainActive = false;
    private bool isBackgroundActive = false;
    private State beforeState;

    private void Awake()
    {
        inst = this;
    }

    /// <summary>
    /// Show Button에 할당하는 함수, open->close close->open
    /// </summary>
    public void OpenClosePhoneUI()
    {
        if(GameManager.inst.ReturnState() != State.Phone)
        {
            beforeState = GameManager.inst.ReturnState();
        }

        if (!isActive)
        {
            isActive = true;
            isMainActive = true;
            isBackgroundActive = true;

            phone.SetActive(isActive);
            blur.SetActive(isActive);

            GameManager.inst.ChangeState(State.Phone);
        }
        else
        {
            ClosePhoneUI();
        }
    }

    public void ClosePhoneUI()
    {
        if (isActive)
        {
            isActive = false;

            //phoneShowButton.SetActive(!isActive);
            phone.SetActive(isActive);
            blur.SetActive(isActive);

            // 인물, 증거, 지도 창을 켜놓은 경우 해당 UI부터 먼저 OFF
            switch (GameManager.inst.ReturnState())
            {
                case State.Map:
                    MapUI.inst.CloseMapUI();
                    break;

                case State.Character:
                    CharacterUI.inst.CloseCharacterUI();
                    break;

                case State.Clue:
                    ClueUI.inst.CloseClueUI();
                    break;
            }

            GameManager.inst.ChangeState(beforeState);
        }
    }

    public void ShowMain()
    {
        if (!isMainActive)
        {
            isMainActive = true;
            
            main.SetActive(isMainActive);
        }
    }

    public void HideMain()
    {
        if (isMainActive)
        {
            isMainActive = false;
            
            main.SetActive(isMainActive);
        }        
    }

    public void ShowBackground()
    {
        if (!isBackgroundActive)
        {
            isBackgroundActive = true;

            background.SetActive(isBackgroundActive);
        }
    }

    public void HideBackground()
    {
        if (isBackgroundActive)
        {
            isBackgroundActive = false;

            background.SetActive(isBackgroundActive);
        }
    }
}
