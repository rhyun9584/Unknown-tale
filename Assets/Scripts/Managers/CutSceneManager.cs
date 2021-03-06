﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{

    public static CutSceneManager inst;


    private string scriptName;
    private int dialogueState;
    private Dialogue dialogue;
    private bool check = false;
    private int sceneNum = 1;

    public GameObject[] background = new GameObject[4];

    public AudioClip[] BGM = new AudioClip[2];

    private AudioSource MP3;


    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        CutSceneNum(sceneNum);
        MP3 = gameObject.GetComponent<AudioSource>();
        MP3.clip = BGM[0];
        MP3.Play();
    }

    private void Update()
    {
        if (sceneNum > 4)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        if (check)
        {
            if(sceneNum == 2)
            {
                MP3.clip = BGM[1];
                MP3.Play();
            }
            CutSceneNum(++sceneNum);
           
            check = false;
        }

        
    }

    public void CutSceneNum(int a)
    {
        scriptName = "CutScene"+a;
        dialogue = LoadDialogue.LoadDialogueData(scriptName);
        dialogueState = 0;

        Debug.Log(a);
        if(a > 0)
            background[a - 1].SetActive(true);
        
        StartCoroutine(Talking());
    }


    IEnumerator Talking()
    {
        DialogueUI.inst.OnDialogue();

        bool next = true;

        for (int i = 0; i < dialogue.talks[dialogueState].Length;)
        {
            if (next)
            {
                if (dialogue.talks[dialogueState][i].portrait == "left")
                {
                    DialogueUI.inst.leftPortrait.SetActive(true);
                    DialogueUI.inst.rightPortrait.SetActive(false);
                }
                else if (dialogue.talks[dialogueState][i].portrait == "right")
                {
                    DialogueUI.inst.leftPortrait.SetActive(false);
                    DialogueUI.inst.rightPortrait.SetActive(true);
                }

                // Image를 포함하는 경우 sentence의 첫 부분에 [Image:(이미지파일이름)]을 flag로 추가
                if (dialogue.talks[dialogueState][i].sentence.Contains("[Image:"))
                {
                    int lastFlagIndex = dialogue.talks[dialogueState][i].sentence.IndexOf("]"); // flag의 마지막의 index

                    string imageFileName = dialogue.talks[dialogueState][i].sentence.Substring(7, lastFlagIndex - 7);
                    DialogueUI.inst.OnDialogueImage(imageFileName);

                    dialogue.talks[dialogueState][i].sentence = dialogue.talks[dialogueState][i].sentence.Substring(lastFlagIndex + 1);
                }
                else
                {
                    DialogueUI.inst.OffDialogueImage();
                }

                DialogueUI.inst.ChangePortraitImage(dialogue.talks[dialogueState][i].portrait == "left", dialogue.talks[dialogueState][i].npccode, dialogue.talks[dialogueState][i].face);
                DialogueUI.inst.ChangeDialogueText(dialogue.talks[dialogueState][i].speaker, dialogue.talks[dialogueState][i].sentence);
               // DialogueUI.inst.ChangeBackgroundImage(dialogue.talks[dialogueState][i].background);

                next = false;
            }
            else if (!next && Input.GetMouseButtonUp(0))
            {
                // UI 버튼 클릭 시 대화가 넘어가지 않도록, 대화창은 Raycast Target을 false로 전환하여 제외
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    next = true;
                    i++;
                }
            }
            yield return null;
        }

        if (dialogueState < dialogue.maxState - 1)
            dialogueState++;

        DialogueUI.inst.OffDialogue();
        check = true;
    }
}
