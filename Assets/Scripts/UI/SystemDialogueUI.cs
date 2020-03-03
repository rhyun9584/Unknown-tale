using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemDialogueUI : MonoBehaviour
{
    public static SystemDialogueUI inst;

    public GameObject dialogueSentence, dialogueImage;

    private Text sentenceText;

    private bool isActive = true;
    private bool isImageActive = true;

    void Awake()
    {
        inst = this;

        sentenceText = dialogueSentence.GetComponentInChildren<Text>();

        OffDialogue();
        OffDialogueImage();
    }

    public void OnDialogue()
    {
        if (!isActive)
        {
            isActive = true;

            dialogueSentence.SetActive(isActive);
        }
    }

    public void OffDialogue()
    {
        if (isActive)
        {
            isActive = false;

            dialogueSentence.SetActive(isActive);
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
    /// change dialogue UI text
    /// </summary>
    public void ChangeDialogueText(string sentence)
    {
        sentenceText.text = sentence;
    }
}