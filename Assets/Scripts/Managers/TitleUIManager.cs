using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public GameObject upperCanvas;
    public GameObject mainCanvas;
    public GameObject optionCanvas;
    public GameObject creditCanvas;
    public GameObject selectCanvas;
    public GameObject[] storyCanvas;

    //아무 키나 누르십시오
    public void OnPressAnyKey() 
    {
        upperCanvas.SetActive(false);
    }

    //이야기 선택 버튼
    public void OnSelectButtonClicked()
    {
        mainCanvas.SetActive(false);
        selectCanvas.SetActive(true); 
    }

    //설정 버튼
    public void OnOptionButtonClicked()
    {
        mainCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }

    //만든이들 버튼
    public void OnCreditButtonClicked()
    {
        mainCanvas.SetActive(false);
        creditCanvas.SetActive(true);
    }

    //뒤로가기 버튼
    public void OnBackButtonClicked()
    {
        mainCanvas.SetActive(true);
        optionCanvas.SetActive(false);
        creditCanvas.SetActive(false);
        selectCanvas.SetActive(false);
    }

    public void onStoryButtonClicked(int a)
    {
        storyCanvas[a].SetActive(true);
    }

    public void onStoryBackButtonClicked(int a)
    {
        storyCanvas[a].SetActive(false);
    }

    public void Update()
    {
        if (Input.anyKey)
        {
            OnPressAnyKey();
        }
    }
}
