using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public static CharacterUI inst;

    public GameObject detailView;
    public GameObject detailName, detailContent;

    //private ScrollRect scrollRect;
    private Text detailNameText, detailContentText;
    private bool isActive = false;
    private bool detailActive = false;

    private void Awake()
    {
        inst = this;
        //scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        detailNameText = detailName.GetComponent<Text>();
        detailContentText = detailContent.GetComponent<Text>();
    }

    public void OpenCharacterUI()
    {
        if (!isActive)
        {
            isActive = true;

            PhoneUI.inst.HideMain();
            gameObject.SetActive(isActive);

            GameManager.inst.ChangeState(State.Character);
        }
    }

    public void CloseCharacterUI()
    {
        if (isActive)
        {
            isActive = false;

            PhoneUI.inst.ShowMain();
            gameObject.SetActive(isActive);

            GameManager.inst.ChangeState(State.Phone);
        }
    }

    public void OnDetailUI(NPCCode npccode)
    {
        if (!detailActive)
        {
            detailActive = true;

            detailView.SetActive(detailActive);

            detailNameText.text = npccode.ToString();
            // detailContentText.text
        }
    }

    public void OffDetailUI()
    {
        if (detailActive)
        {
            detailActive = false;

            detailView.SetActive(detailActive);
        }
    }
}
