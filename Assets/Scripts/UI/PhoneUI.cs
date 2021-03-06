﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUI : MonoBehaviour
{
    public static PhoneUI inst;

    public GameObject phoneShowButton;
    public GameObject phone, main, background, blur;
    
    private Image backgroundImage;

    private bool isActive = true;
    private bool isMainActive = false;
    private bool isBackgroundActive = false;
    private State beforeState;


    private void Awake()
    {
        inst = this;

        isActive = true;
        backgroundImage = background.GetComponent<Image>();
    }

    private void Start()
    {
        ClueUI.inst.CloseClueUI();
        CharacterUI.inst.CloseCharacterUI();
        OpenClosePhoneUI();
    }
    /// <summary>
    /// Show Button에 할당하는 함수, open->close close->open
    /// </summary>
    public void OpenClosePhoneUI()
    {
        if (!isActive)
        {
            if(GameManager.inst.ReturnState() != State.Phone)
            {
                beforeState = GameManager.inst.ReturnState();
            }

            isActive = true;
            isMainActive = true;
            isBackgroundActive = true;

            phone.SetActive(isActive);
            blur.SetActive(isActive);

            phoneShowButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/phone/phone on") as Sprite;

            TutorialManager.inst.block.SetActive(false); // 강제로 폰을 열도록 만든 블락 이미지 끄기

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

            phoneShowButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/phone/phone off") as Sprite;

            GameManager.inst.ChangeState(beforeState);
        }
    }

    public void ChangeBackgroundImage(string route)
    {
        backgroundImage.sprite = Resources.Load<Sprite>(route);
    }

    public void ShowMain()
    {
        if (!isMainActive)
        {
            isMainActive = true;
            ChangeBackgroundImage("UI/phone/main/background");

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
