using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneUI : MonoBehaviour
{
    public static PhoneUI inst;

    public GameObject phoneShowButton;

    [Header("phone UI")]
    public GameObject phone;

    private bool isActive = false;
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
            phone.SetActive(isActive);

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

            // 인물, 증거, 지도 창을 켜놓은 경우 해당 UI부터 먼저 OFF
            switch (GameManager.inst.ReturnState())
            {
                case State.Map:
                    MapUI.inst.CloseMapUI();
                    break;

                case State.Character:
                    CharacterUI.inst.CloseCharacterUI();
                    break;
            }

            GameManager.inst.ChangeState(beforeState);
        }
    }

}
