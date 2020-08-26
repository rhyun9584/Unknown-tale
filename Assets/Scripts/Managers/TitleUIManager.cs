using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public GameObject upperCanvas;
    public GameObject mainCanvas;
 //   public GameObject optionCanvas;
    public GameObject creditCanvas;
    public GameObject selectCanvas;
    public GameObject table;
    public GameObject[] storyCanvas;
    public SoundManager soundManager; 

    //아무 키나 누르십시오
    public void OnPressAnyKey() 
    {
        upperCanvas.SetActive(false);
    }

    public void OnTableButtonClicked()
    {
        table.SetActive(true);
    }

    //이야기 선택 버튼
    public void OnSelectButtonClicked()
    {
        mainCanvas.SetActive(false);
        selectCanvas.SetActive(true);
        soundManager.PlayDrawerOpenSound();
    }

    //설정 버튼
    public void OnOptionButtonClicked()
    {
        mainCanvas.SetActive(false);
    //    optionCanvas.SetActive(true);
        soundManager.PlayDrawerOpenSound();
    }

    //만든이들 버튼
    public void OnCreditButtonClicked()
    {
        mainCanvas.SetActive(false);
        creditCanvas.SetActive(true);
        soundManager.PlayDrawerOpenSound();
    }

    //뒤로가기 버튼
    public void OnBackButtonClicked()
    {
        mainCanvas.SetActive(true);
       // optionCanvas.SetActive(false);
        creditCanvas.SetActive(false);
        selectCanvas.SetActive(false);
        soundManager.PlayDrawerCloseSound();
    }

    public void onStoryButtonClicked(int a)
    {
        storyCanvas[a].SetActive(true);
    }

    public void onStoryBackButtonClicked(int a)
    {
        storyCanvas[a].SetActive(false);
    }

    //임시 별주부전 시작 버튼
    public void onGameStartButton()
    {
        SceneManager.LoadScene("CutScene");
    }

    public void Start()
    {
        upperCanvas.SetActive(true);
        mainCanvas.SetActive(true);
    }

    public void Update()
    {
        if (Input.anyKey)
        {
            OnPressAnyKey();
        }
    }
}
