using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager inst;

    private string scriptName;
    private int dialogueState;
    private Dialogue dialogue;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        TutorialStart();
    }

    public void TutorialStart()
    {
        scriptName = "Tutorial1";
        dialogue = LoadDialogue.LoadDialogueData(scriptName);
        dialogueState = 0;
        
        StartCoroutine(Talking());
    }

    IEnumerator Talking()
    {
        GameManager.inst.ChangeState(State.Talk);
        DialogueUI.inst.OnDialogue();

        bool next = true;

        for (int i = 0; i < dialogue.talks.Length;)
        {
            if (next)
            {
                if(dialogue.talks[i].portrait == "left")
                {
                    DialogueUI.inst.leftPortrait.SetActive(true);
                    DialogueUI.inst.rightPortrait.SetActive(false);
                }
                else if(dialogue.talks[i].portrait == "right")
                {
                    DialogueUI.inst.leftPortrait.SetActive(false);
                    DialogueUI.inst.rightPortrait.SetActive(true);
                }

                DialogueUI.inst.ChangePortraitImage(dialogue.talks[i].portrait == "left", dialogue.talks[i].npccode, dialogue.talks[i].face);
                DialogueUI.inst.ChangeDialogueText(dialogue.talks[i].speaker, dialogue.talks[i].sentence);
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
        GameManager.inst.ChangeState(State.NpcSearch);
    }
}
