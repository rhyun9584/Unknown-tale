using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NPCBase : MonoBehaviour
{
    [SerializeField]
    private NPCCode npccode;
    [SerializeField]
    private string npcname;

    private int dialogueState; // 마지막 대화 index
    private Dialogue dialogue;

    void Start()
    {
        //LoadDialogue.LoadDialogueData(npcname, npccode);
        //dialogue = LoadDialogue.dialogues[(int)npccode];
        dialogue = LoadDialogue.LoadDialogueData(npcname);
        dialogueState = 0;
    }

    public void OpenDialog()
    {
        GameManager.inst.ChangeState(State.Talk);
        DialogueUI.inst.OnDialogue();
        
        StartCoroutine(Talking());
    }

    IEnumerator Talking()
    {
        bool next = true;

        for(int i = 0; i < dialogue.talks.Length;)
        {
            if (next)
            {
                DialogueUI.inst.ChangeDialogueText(dialogue.talks[i].speaker, dialogue.talks[i].sentence);
                next = false;
            }
            else if(!next && Input.GetMouseButtonUp(0) && GameManager.inst.ReturnState() == State.Talk)
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
        
        if (dialogueState == 0)
        {
            NPCManager.inst.SetNpcActive(npccode);
        }
        if (dialogueState < dialogue.maxState - 1)
            dialogueState++;

        DialogueUI.inst.OffDialogue();
        GameManager.inst.ChangeState(State.NpcSearch);
    }
}
