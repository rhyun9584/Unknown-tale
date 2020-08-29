using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReasonManager : MonoBehaviour
{
    private Dialogue dialogue;

    public string scriptName;

    private int dialogueState;

    private LocationCode currentLocation;

    public Button choice0, choice1;
    public Image errorText;
    public Sprite forText;

    private void Awake()
    {
        choice0.gameObject.SetActive(false);
        choice1.gameObject.SetActive(false);
        errorText.gameObject.SetActive(false);
    }

    public void DecideReason()
    {
        StartCoroutine(CheckDecideReason());
    }

    IEnumerator CheckDecideReason() //decide start reason or not
    {
        bool reasonStart = false;

        ButtonSetting(0, "", "추리 시작!");
        ButtonSetting(1, "", "지금은 아닌것 같다.");
        yield return StartCoroutine(waitChoice());
        if(choice0.gameObject.GetComponentInChildren<Choicebutton>().clk)
        {
            reasonStart = true;
        }
        else if(choice1.gameObject.GetComponentInChildren<Choicebutton>().clk)
        {
            reasonStart = false;
        }
        yield return new WaitForSeconds(0.1f);

        if(reasonStart)
        {
            errorText.gameObject.SetActive(true);
            errorText.GetComponentInChildren<Text>().text = "추리를 시작 하자!";
            yield return new WaitForSeconds(0.5f);
            errorText.gameObject.SetActive(false);

            currentLocation = GameManager.inst.ReturnLocation();

            switch (currentLocation)
            {
                case LocationCode.PartyHall: ReasonFirst();
                    break;

                default:
                    break;
            }
        }
        else
        {

        }
        yield return null;
    }

    public void ReasonFirst() //for tutorial
    {
        GameManager.inst.ChangeState(State.Reasoning);

        scriptName = "Reason/ReasonForTutorial";
        dialogue = LoadDialogue.LoadDialogueData(scriptName);
        dialogueState = 0;

        StartCoroutine(Talking());
    }

    public void ButtonSetting(int a, string image, string text)
    {
        if (a == 0)
        {
            choice0.GetComponentInChildren<Choicebutton>().clk = false;
            choice0.gameObject.SetActive(true);
            if (image == "")
            {
                choice0.image.sprite = forText;
            }
            else
            {
                //choice0.image.preserveAspect = true;
                choice0.image.sprite = Resources.Load<Sprite>("UI/clue/" + image);
            }
            choice0.GetComponentInChildren<Text>().text = text;
        }
        else if (a == 1)
        {
            choice1.GetComponentInChildren<Choicebutton>().clk = false;
            choice1.gameObject.SetActive(true);
            if (image == "")
            {
                choice1.image.sprite = forText;
            }
            else
            {
                choice1.image.preserveAspect = true;
                choice1.image.sprite = Resources.Load<Sprite>("UI/clue/" + image);
            }
            choice1.GetComponentInChildren<Text>().text = text;
        }
    }

    public void turnOffButton()
    {
        choice0.gameObject.SetActive(false);
        choice1.gameObject.SetActive(false);
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
                if(i == 0) //임시 숫자
                {
                    ButtonSetting(0, "", "범인이 아닙니다!");
                    ButtonSetting(1, "", "범인 입니다!");
                    yield return StartCoroutine(waitChoiceWithOnlyCorrect());
                    next = true;
                    i++;
                    continue;
                }else if(i == 1)
                {
                    ButtonSetting(0, "1", "");
                    ButtonSetting(1, "3", "");
                    yield return StartCoroutine(waitChoiceWithOnlyCorrect());
                    next = true;
                    i++;
                    continue;
                }else if(i == 8)
                {
                    ButtonSetting(0, "2", "");
                    ButtonSetting(1, "1", "");
                    yield return StartCoroutine(waitChoiceWithOnlyCorrect());
                    next = true;
                    i++;
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

        // 튜토리얼 끝나고 바로 엔딩으로
        SceneManager.LoadScene("EndingScene");
    }

    IEnumerator waitChoiceWithOnlyCorrect() //only zero is correct
    {
        while (!choice0.GetComponentInChildren<Choicebutton>().clk)
        {
            if (choice1.GetComponentInChildren<Choicebutton>().clk)
            {
                errorText.gameObject.SetActive(true);
                errorText.GetComponentInChildren<Text>().text = "이게 아니야, 다시 생각해보자.";
                choice0.gameObject.SetActive(false);
                choice1.gameObject.SetActive(false);
                yield return new WaitForSeconds(1f);
                errorText.gameObject.SetActive(false);
                choice0.gameObject.SetActive(true);
                choice1.gameObject.SetActive(true);
                choice1.GetComponentInChildren<Choicebutton>().clk = false;
            }
            yield return new WaitForSeconds(0.01f);
        }
        turnOffButton();
    }

    IEnumerator waitChoice()
    {
        while (!choice0.GetComponentInChildren<Choicebutton>().clk & !choice1.GetComponentInChildren<Choicebutton>().clk)
        {
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
