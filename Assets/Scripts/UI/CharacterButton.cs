using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [SerializeField]
    private NPCCode npccode;
    private string npcname;
    
    private Text characterName;
    private Image characterImage;

    private void Awake()
    {
        characterName = GetComponentInChildren<Text>();
        characterImage = transform.Find("Image").gameObject.GetComponentInChildren<Image>();
    }

    /// <summary>
    /// 버튼의 image와 text를 해당 npc에 맞게 교체
    /// </summary>
    public void OpenButton(Npc npcData)
    {
        npcname = npcData.npcName;
        npccode = npcData.npcCode;

        characterName.text = npcname;
        characterImage.sprite = Resources.Load<Sprite>("UI/npc icon/" + ((int)npccode).ToString()) as Sprite;
    }

    public void OpenButtonMain()
    {
        npcname = "주인공";
        npccode = NPCCode.MAIN;
    }

    public void OpenDetailUI()
    {
        if (NPCManager.inst.npcActive[(int)npccode])
        {
            CharacterUI.inst.OnDetailUI(npccode, npcname);
        }
        else
        {
            Debug.Log("This NPC is not activated");
        }
    }
}
