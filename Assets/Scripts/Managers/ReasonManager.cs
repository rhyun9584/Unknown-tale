using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReasonManager : MonoBehaviour
{
    private Dialogue dialogue;

    public string scriptName;
    private int dialogueState;

    public Button choice0, choice1;
    public Choicebutton c0, c1;
    public Text errorText;

    private void Awake()
    {
        choice0.enabled = false;
        choice1.enabled = false;
        errorText.enabled = false;
    }

    public void ReasonFirst() //for tutorial
    {
        scriptName = "Reason/ReasonForTutorial";
        dialogue = LoadDialogue.LoadDialogueData(scriptName);
        dialogueState = 0;

        StartCoroutine(Talking());
    }

    public void ButtonSetting(int a, string image, string text)
    {
        if (a == 0)
        {
            c0.clk = false;
            choice0.enabled = true;
            if (image == "")
            {
                //choice0.image = 
                //ui 브랜치와 머지후 핸드폰 갤러리 칸으로 대체
            }
            else
                choice0.image = Resources.Load<Image>("UI/clue" + image);
            choice0.GetComponentInChildren<Text>().text = text;
        }
        else if (a == 1)
        {
            c1.clk = false;
            choice1.enabled = true;
            if (image == "")
            {
                //choice0.image = 
                //ui 브랜치와 머지후 핸드폰 갤러리 칸으로 대체
            }
            else
                choice1.image = Resources.Load<Image>("UI/clue" + image);
            choice1.GetComponentInChildren<Text>().text = text;
        }
    }

    public void turnOffButton()
    {
        choice0.enabled = false;
        choice1.enabled = false;
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

                next = false;
            }
            else if (!next && Input.GetMouseButtonUp(0) && GameManager.inst.ReturnState() == State.Talk)
            {
                if(i == 15) //임시 숫자
                {
                    ButtonSetting(0, "0", "");
                    ButtonSetting(1, "1", "");
                    yield return StartCoroutine(waitChoice());
                    /*
                     next = true;
                     i++;
                     continue;
                     */ 
                }
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

    IEnumerator waitChoice() //only zero is correct
    {
        while (!c0.clk)
        {
            if (c1.clk)
            {
                errorText.enabled = true;
                errorText.text = "잘못된 선택입니다!";
                yield return new WaitForSeconds(1f);
                errorText.enabled = false;
                c1.clk = false;
            }
            yield return new WaitForSeconds(0.01f);
        }
        turnOffButton();
    }

    /*
    필요한 것 :
    대화 진행을 위한 dialogue
    갤러리 호출 및, 단서 상황 확인을 위한 ( )
    선택지 저장 및 분기 구분
    */


}
