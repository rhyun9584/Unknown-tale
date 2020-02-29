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

    /// <summary>
    /// 버튼의 image와 text를 해당 npc에 맞게 교체
    /// </summary>
    public void OpenButton(string npcName)
    {
        characterName = GetComponentInChildren<Text>();
        characterImage = GetComponentInChildren<Image>();

        characterName.text = npcName;
        npcname = npcName;
        //characterImage.sprite = Resources.Load("/Detail/" + npccode.ToString() + ".png") as Sprite; // npc detail 내 image 파일이름 정해야함
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
