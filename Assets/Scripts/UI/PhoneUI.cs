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

    private void Awake()
    {
        inst = this;
    }

    public void OpenPhoneUI()
    {
        if (!isActive)
        {
            isActive = true;

            phoneShowButton.SetActive(!isActive);
            phone.SetActive(isActive);

            GameManager.inst.ChangeState(State.Phone);
        }
    }

    public void ClosePhoneUI()
    {
        if (isActive)
        {
            isActive = false;

            phoneShowButton.SetActive(!isActive);
            phone.SetActive(isActive);

            MapUI.inst.CloseMapUI();
            GameManager.inst.ChangeState(State.Search);
        }
    }

}
