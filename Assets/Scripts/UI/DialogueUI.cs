﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI inst;

    public GameObject dialogueName, dialogueSentence, leftPortrait, rightPortrait, dialogueImage, background;

    public Font baseFont;

    private Text nameText, sentenceText;
    private Image leftPortraitImage, rightPortraitImage;

    private bool isActive = true;
    private bool isImageActive = true;

    void Awake()
    {
        inst = this;

        nameText = dialogueName.GetComponent<Text>();
        sentenceText = dialogueSentence.GetComponentInChildren<Text>();

        leftPortraitImage = leftPortrait.GetComponent<Image>();
        rightPortraitImage = rightPortrait.GetComponent<Image>();
        
        OffDialogue();
        OffDialogueImage();
    }

    public void OnDialogue()
    {
        if (!isActive)
        {
            isActive = true;

            dialogueName.SetActive(isActive);
            dialogueSentence.SetActive(isActive);
            background.SetActive(isActive);
        }
    }

    public void OffDialogue()
    {
        if (isActive)
        {
            isActive = false;

            dialogueName.SetActive(isActive);
            dialogueSentence.SetActive(isActive);
            background.SetActive(isActive);

            leftPortrait.SetActive(isActive);
            rightPortrait.SetActive(isActive);
        }
    }

    public void OnDialogueImage(string imageFileName)
    {
        //Debug.Log(imageFileName);
        dialogueImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Dialogue/Image/" + imageFileName);

        if (!isImageActive)
        {
            isImageActive = true;
            
            dialogueImage.SetActive(isImageActive);
            dialogueImage.GetComponent<Image>().preserveAspect = true;
        }
    }

    public void OffDialogueImage()
    {
        if (isImageActive)
        {
            isImageActive = false;

            dialogueImage.SetActive(isImageActive);
        }
    }

    /// <summary>
    /// change dialogue UI name, text
    /// </summary>
    public void ChangeDialogueText(string name, string sentence)
    {
        nameText.text = name;
        sentenceText.text = sentence;
    }

    public void ChangeDialogueTextFont(string location, int code)
    {
        if (code < 1)
        {
            sentenceText.font = baseFont;
        }
        else
        {
            string[] npcName = {
            "MAIN",
            "Ocean King",
            "Octopus",
            "Ocean Son",
            "Angler",
            "Anchovy",
            "Mountain King" };

            Font charaFont = Resources.Load<Npc>("NPC/" + (location + "_" + npcName[code])).charaFont;

            if (charaFont == null)
            {
                sentenceText.font = baseFont;
            }
            else
            {
                sentenceText.font = charaFont;
            }
        }
    }

    public void ChangePortraitImage(bool isLeft, int npcCode, int face)
    {
        if (isLeft)
        {
            leftPortraitImage.sprite = Resources.Load<Sprite>("Portrait/" + npcCode.ToString() + "_" + face.ToString() + "_L");
            leftPortraitImage.preserveAspect = true;
        }
        else
        {
            rightPortraitImage.sprite = Resources.Load<Sprite>("Portrait/" + npcCode.ToString() + "_" + face.ToString() + "_R");
            rightPortraitImage.preserveAspect = true;
        }
    }

}
