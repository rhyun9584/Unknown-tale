using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;

    public GameObject[] map;

    void Awake()
    {
        if (UIManager.inst == null)
            UIManager.inst = this;
    }

    public void OnUI(MapCode mapcode)
    {
        map[(int)mapcode].SetActive(true);
    }

    public void OffUI(MapCode mapcode)
    {
        map[(int)mapcode].SetActive(false);
    }
}
