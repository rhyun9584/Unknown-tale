using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager inst;

    public static int npcCount = System.Enum.GetValues(typeof(NPCCode)).Length;

    public GameObject[] characterButtons;

    // npc가 한 번이라도 대화를 진행한 적이 있는지
    [HideInInspector]
    public bool[] npcActive = new bool[npcCount];

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        for (int i = 0; i < npcCount; i++)
        {
            npcActive[i] = false;
        }

        SetNpcActive(NPCCode.MAIN); // 주인공은 바로 active
    }

    /// <summary>
    /// npcActive를 true로 변경하고 Phone 내부 character UI의 button을 활성화 시킴
    /// </summary>
    /// <param name="npccode"></param>
    public void SetNpcActive(NPCCode npccode)
    {
        CharacterButton characterButtonScript = characterButtons[(int)npccode].GetComponent<CharacterButton>();
        
        npcActive[(int)npccode] = true;

        characterButtonScript.OpenButton();
    }

}
