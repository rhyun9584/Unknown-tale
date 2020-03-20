using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NPCBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Npc npcData;

    private int dialogueState; // 마지막 대화 index
    private Dialogue dialogue;

    private Vector2 hotSpot;
    private Texture2D cursor;

    void Awake()
    {
        npcData = Resources.Load<Npc>("NPC/" + ((int)GameManager.inst.ReturnLocation()).ToString() + "_" + this.gameObject.name);
    }

    void Start()
    {
        //LoadDialogue.LoadDialogueData(npcname, npccode);
        //dialogue = LoadDialogue.dialogues[(int)npccode];
        dialogue = LoadDialogue.LoadDialogueData("npc/" + ((int)npcData.locationCode).ToString() + "_" + ((int)npcData.npcCode).ToString());
        dialogueState = 0;

        cursor = GameManager.inst.npcCursor;

        hotSpot.x = cursor.width / 2;
        hotSpot.y = cursor.height / 2;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(GameManager.inst.ReturnState() == State.NpcSearch)
        {
            Cursor.SetCursor(cursor, hotSpot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OpenDialog()
    {
        if(GameManager.inst.ReturnState() == State.NpcSearch)
        {
            StartCoroutine(Talking());
        }
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
            NPCManager.inst.SetNpcActive(npcData.npcCode);
            
            //Phone 내부 character UI의 button을 활성화 시킴
            CharacterUI.inst.characterSlots[(int)npcData.npcCode].GetComponent<CharacterButton>().OpenButton(npcData);
        }
        for(int i = 0; i < npcData.npcExplains.Count; i++)
        {
            if(npcData.npcExplains[i].state == dialogueState)
            {
                CharacterUI.inst.AddExplain((int)npcData.npcCode, npcData.npcExplains[i].explain);
                break;
            }
        }
        if (dialogueState < dialogue.maxState - 1)
        {
            dialogueState++;
        }

        DialogueUI.inst.OffDialogue();
        GameManager.inst.ChangeState(State.NpcSearch);
    }
}
