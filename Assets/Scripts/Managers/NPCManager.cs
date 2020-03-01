using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager inst;

    public static int npcCount = System.Enum.GetValues(typeof(NPCCode)).Length;

    // npc가 한 번이라도 대화를 진행한 적이 있는지
    [HideInInspector] 
    public bool[] npcActive;// = new bool[npcCount];

    private void Awake()
    {
        inst = this;
        npcActive = new bool[npcCount];
    }

    void Start()
    {
        // dummy 뛰어넘고 4번부터
        for (int i = 4; i < npcCount; i++)
        {
            npcActive[i] = false;
        }

        // 주인공은 바로 active
        SetNpcActive(NPCCode.MAIN); 
        CharacterUI.inst.characterSlots[0].GetComponent<CharacterButton>().OpenButton("주인공");
    }

    /// <summary>
    /// npcActive를 true로 변경
    /// </summary>
    /// <param name="npccode"></param>
    public void SetNpcActive(NPCCode npccode)
    {
        npcActive[(int)npccode] = true;
    }
}
