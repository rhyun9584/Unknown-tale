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
        characterSlots = new GameObject[NPCManager.npcCount];

        // 주인공 slot
        characterSlots[0] = transform.Find("Main").Find("MainChar Slot").gameObject;

        // npc slot
        for(int i = 1; i < NPCManager.npcCount; i++)
        {
            characterSlots[i] = transform.Find("Main").Find("Scroll View").Find("Viewport").Find("Content").Find("Slot " + i.ToString()).gameObject;
        }

        AddExplain(0, "현장학습으로 온 고궁에서 서고에 들어갔다가 이상한 곳으로 빨려 들어온 불쌍한 고등학생이다.");
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
            PhoneUI.inst.ChangeBackgroundImage("UI/phone/people1/background");
  
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
