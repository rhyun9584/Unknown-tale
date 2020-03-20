using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public static CharacterUI inst;

    public GameObject detailView;
    public GameObject detailName, detailContent, detailImage;
    public GameObject[] characterSlots;

    // npc에 대한 explain을 한 번에 관리
    private string[] npcExplains;
    private Text detailNameText, detailContentText;
    private bool isActive = true;
    private bool detailActive = false;

    private void Awake()
    {
        inst = this;

        npcExplains = new string[NPCManager.npcCount];
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

            OffDetailUI();

            PhoneUI.inst.ShowMain();
            gameObject.SetActive(isActive);

            GameManager.inst.ChangeState(State.Phone);
        }
    }

    public void OnDetailUI(NPCCode npccode, string npcname)
    {
        if (!detailActive)
        {
            detailActive = true;

            detailView.SetActive(detailActive);

            detailNameText.text = npcname;
            detailContentText.text = npcExplains[(int)npccode];
            detailImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/npc icon/" + ((int)npccode).ToString()) as Sprite;
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

    public void AddExplain(int npccode, string explain)
    {
        npcExplains[npccode] += explain + "\n";
    }
}
