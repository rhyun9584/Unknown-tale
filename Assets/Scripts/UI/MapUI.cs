﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public static MapUI inst;

    public GameObject mapUI;
    public GameObject exitButton, moveButton, background;

    private bool isActive = false;

    void Awake()
    {
        if (inst == null)
            inst = this;

        //isActive = false;
        //CloseMapUI();
    }

    public void OpenMapUI()
    {
        if (!isActive)
        {
            isActive = true;

            PhoneUI.inst.HideMain();
            PhoneUI.inst.HideBackround();
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
            PhoneUI.inst.ShowBackround();
            mapUI.SetActive(isActive);

            GameManager.inst.ChangeState(State.Phone);
        }
    }
}
