using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        LoadDialogue.LoadDialogueData(npcname, npccode);
        dialogue = LoadDialogue.dialogues[(int)npccode];
        dialogueState = 0;
    }

    public void OpenDialog()
    {
        GameManager.inst.ChangeState(State.Talk);
        DialogueUI.inst.OnOffDialogue();
        
        StartCoroutine(Talking());
    }

    IEnumerator Talking()
    {
        bool next = true;

        for(int i = 0; i < dialogue.sentences[this.dialogueState].Length;)
        {
            if (next)
            {
                DialogueUI.inst.ChangeDialogueText(npcname, dialogue.sentences[this.dialogueState][i]);
                next = false;
            }
            else if(!next && Input.GetMouseButtonUp(0))
            {
                next = true;
                i++;
            }

            yield return null;
        }

        if (dialogueState < dialogue.maxState - 1)
            dialogueState++;

        DialogueUI.inst.OnOffDialogue();
        GameManager.inst.ChangeState(State.Search);
    }
}
