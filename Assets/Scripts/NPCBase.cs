using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour
{
    public NPCCode NPCCode;
    public string NPCName;

    private int dialogueState; // 마지막 대화 index
    private Dialogue dialogue;
    
    void Start()
    {
        LoadDialogue.LoadDialogueData(); // 임시

        dialogue = LoadDialogue.dialogue[(int)NPCCode];
        dialogueState = 0;

    }

    public void OpenDialog()
    {
        for(int i = 0; dialogue.sentences[this.dialogueState][i] != null; i++)
            Debug.Log(dialogue.sentences[this.dialogueState][i]);
        if (dialogueState < dialogue.maxState - 1)
            dialogueState++;
    }
}
