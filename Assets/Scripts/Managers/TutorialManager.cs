using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager inst;
    public TemporaryImage TemporaryImage;
    public NPCManager NPCManager;

    private string scriptName;
    private int dialogueState;
    private Dialogue dialogue;
    public ClueBase footprint;
    public ClueBase clear;

    private bool[] checkClueObtain = {true, true, true, true, true};

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        TutorialStart();
    }

    private void Update()
    {
        if (ClueManager.inst.isObtain[2] && checkClueObtain[0]) // 2 -> foot print
        {
            TutorialSecond();
        }
        else if (ClueManager.inst.isObtain[3] && checkClueObtain[1]) // 3 -> clean floor 
        {
            TutorialThird();
        }

    }

    public void TutorialStart()
    {
        scriptName = "Tutorial1";
        dialogue = LoadDialogue.LoadDialogueData(scriptName);
        dialogueState = 0;
        
        StartCoroutine(Talking());
    }

    public void TutorialSecond()
    {
        checkClueObtain[0] = false;
        scriptName = "Tutorial2";
        dialogue = LoadDialogue.LoadDialogueData(scriptName);
        dialogueState = 0;

        StartCoroutine(Talking());
    }

    public void TutorialThird()
    {
        checkClueObtain[1] = false;
        scriptName = "Tutorial3";
        dialogue = LoadDialogue.LoadDialogueData(scriptName);
        dialogueState = 0;

        StartCoroutine(Talking());
    }

    IEnumerator Talking()
    {
        GameManager.inst.ChangeState(State.Talk);
        DialogueUI.inst.OnDialogue();

        bool next = true;

        for (int i = 0; i < dialogue.talks[dialogueState].Length;)
        {
            if (next)
            {
                if(dialogue.talks[dialogueState][i].portrait == "left")
                {
                    DialogueUI.inst.leftPortrait.SetActive(true);
                    DialogueUI.inst.rightPortrait.SetActive(false);
                }
                else if(dialogue.talks[dialogueState][i].portrait == "right")
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
                
                /*
                if (checkClueObtain[0])
                {
                    if (i == 11)
                        TemporaryImage.gameObject.SetActive(true);
                    else if (i == 12)
                    {
                        TemporaryImage.gameObject.SetActive(false);
                        TemporaryImage.gameObject.SetActive(true);
                    }
                    else if (i == 14)
                    {
                        TemporaryImage.gameObject.SetActive(false);
                        TemporaryImage.gameObject.SetActive(true);
                    }
                    else if (i == 16)
                        TemporaryImage.gameObject.SetActive(false);
                    else if (i == 17)
                        TemporaryImage.gameObject.SetActive(true);
                    else if (i == 19)
                        TemporaryImage.gameObject.SetActive(false);
                }
                else if (checkClueObtain[0] != checkClueObtain[1])
                {
                    if (i == 2)
                        TemporaryImage.gameObject.SetActive(true);
                    else if (i == 3)
                        TemporaryImage.gameObject.SetActive(false);
                }
                else if (checkClueObtain[1] != checkClueObtain[2])
                    if (i == 6)
                        NPCManager.npcActive[7] = true;
                        
    */
                next = false;
            }
            else if (!next && Input.GetMouseButtonUp(0) && GameManager.inst.ReturnState() == State.Talk)
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

        GameManager.inst.ChangeState(State.ClueSearch);
    }
}
