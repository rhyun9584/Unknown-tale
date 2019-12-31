using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;

    public GameObject[] MAP;

    void Awake()
    {
        if (UIManager.inst == null)
            UIManager.inst = this;
    }

    public void OnUI(MapCode mapcode)
    {
        MAP[(int)mapcode].SetActive(true);
    }

    public void OffUI(MapCode mapcode)
    {
        MAP[(int)mapcode].SetActive(false);
    }
}
