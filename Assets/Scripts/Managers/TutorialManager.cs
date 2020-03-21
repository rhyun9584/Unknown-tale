using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager inst;
    public NPCManager NPCManager;

    public Npc anglerData;

    public GameObject secondClue;

    private string scriptName;
    private int dialogueState;
    private Dialogue dialogue;
    public ClueBase footprint;
    public ClueBase clear;

    private bool[] checkTrigger = {false, false, false, false};

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
        if (ClueManager.inst.isObtain[2] && !checkTrigger[0]) // 2 -> foot print
        {
            TutorialSecond();
        }
        else if (ClueManager.inst.isObtain[3] && !checkTrigger[1]) // 3 -> clean floor 
        {
            TutorialThird();
        }
        else if (NPCManager.inst.npcActive[(int)NPCCode.Angler] && !checkTrigger[2] && GameManager.inst.ReturnState() == State.NpcSearch)
        {
            TutorialFourth();
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
        checkTrigger[0] = true;
        scriptName = "Tutorial2";
        dialogue = LoadDialogue.LoadDialogueData(scriptName);
        dialogueState = 0;

        StartCoroutine(Talking());

        secondClue.SetActive(true); // 두 번째 단서를 얻어야 할 시점에서만 active되도록 
    }

    public void TutorialThird()
    {
        checkTrigger[1] = true;
        scriptName = "Tutorial3";
        dialogue = LoadDialogue.LoadDialogueData(scriptName);
        dialogueState = 0;

        StartCoroutine(Talking());

        /*
        // 튜토리얼 진행 중 강제적으로 아귀대신과 말하게되어 바로 active 시킴
        NPCManager.inst.SetNpcActive(NPCCode.Angler);
        CharacterUI.inst.characterSlots[4].GetComponent<CharacterButton>().OpenButton(anglerData);
        CharacterUI.inst.AddExplain(4, "나를 본 적이 없다고 한다.");
        */
    }
    public void TutorialFourth()
    {
        checkTrigger[2] = true;
        scriptName = "Tutorial4";
        dialogue = LoadDialogue.LoadDialogueData(scriptName);
        dialogueState = 0;

        StartCoroutine(Talking());
    }

    IEnumerator Talking()
    {
        LocationManager.inst.OffObject();   // 대화 전 clue와 npc 오브젝트 전부 끔

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
        if (!checkTrigger[1])
            GameManager.inst.ChangeState(State.ClueSearch);
        else if (!checkTrigger[2])
            GameManager.inst.ChangeState(State.ClueSearch);
        else
            GameManager.inst.ChangeState(State.NpcSearch);

        LocationManager.inst.SearchUIChange();  // 다시 적절한 clue 혹은 npc 오브젝트 활성화
    }
}
