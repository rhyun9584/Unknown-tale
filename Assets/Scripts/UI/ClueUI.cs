using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueUI : MonoBehaviour
{
    public static ClueUI inst;

    private bool isActive = false;

    private void Awake()
    {
        inst = this;
    }

    public void OpenClueUI()
    {
        if (!isActive)
        {
            isActive = true;

            PhoneUI.inst.HideMain();
            gameObject.SetActive(isActive);

            GameManager.inst.ChangeState(State.Clue);
        }
    }

    public void CloseClueUI()
    {
        if (isActive)
        {
            isActive = false;

            PhoneUI.inst.ShowMain();
            gameObject.SetActive(isActive);

            GameManager.inst.ChangeState(State.Phone);
        }

    }
}
