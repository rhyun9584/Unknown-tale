using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "locationCode_npcNumber_npcName", menuName = "Scriptable Object/NPC Data")]
public class Npc : ScriptableObject
{
    public NPCCode npcCode;
    public string npcName;
}
