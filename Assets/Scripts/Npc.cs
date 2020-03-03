using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NpcExplain
{
    public int state;       
    public string explain;
}

[CreateAssetMenu(fileName = "locationCode_npcNumber_npcName", menuName = "Scriptable Object/NPC Data")]
public class Npc : ScriptableObject
{
    public LocationCode locationCode;
    public NPCCode npcCode;
    public string npcName;

    public List<NpcExplain> npcExplains = new List<NpcExplain>();
}
