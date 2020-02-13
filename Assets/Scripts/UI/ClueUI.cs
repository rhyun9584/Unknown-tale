using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueUI : MonoBehaviour
{
    public static ClueUI inst;

    public GameObject detailUI, detailImage, detailName, detailExplain;
    public GameObject[] clueSlots;

    private Text nameText, explainText;

    private bool isActive = true;
    private bool isDetail = false;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        nameText = detailName.GetComponent<Text>();
        explainText = detailExplain.GetComponent<Text>();
    }

    public void OpenClueUI()
    {
        if (!isActive)
        {
            isActive = true;

            PhoneUI.inst.HideMain();
            gameObject.SetActive(isActive);

            GameManager.inst.ChangeState(State.Clue);
        }
    }

    public void CloseClueUI()
    {
        if (isActive)
        {
            isActive = false;

            CloseDetailUI();

            PhoneUI.inst.ShowMain();
            gameObject.SetActive(isActive);

            GameManager.inst.ChangeState(State.Phone);
        }

    }

    public void OpenDetailUI(int clueNumber, string clueName)
    {
        if (!isDetail)
        {
            isDetail = true;

            nameText.text = clueName;
            explainText.text = ClueManager.inst.clueExplainTexts[clueNumber];

            detailUI.SetActive(isDetail);
        }
    }

    public void CloseDetailUI()
    {
        if (isDetail)
        {
            isDetail = false;

            detailUI.SetActive(isDetail);
        }
    }

    public void BackButton()
    {
        if (isDetail)
        {
            CloseDetailUI();
        }
        else if (isActive)
        {
            CloseClueUI();
        }
    }
}
