using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

public class LoadDialogue
{
    public static int numOfNpc = 2;

    public static Dialogue[] dialogues = new Dialogue[numOfNpc];

    /// <summary>
    /// Load Dialogue saved as Json
    /// 각 NPC가 처음 활성화될때 각자의 dialogue를 불러옴
    /// </summary>
    public static void LoadDialogueData(string name, NPCCode npcCode)
    {
        Debug.Log("Loading " + name + "'s dialogues");
        string loadString = Resources.Load<TextAsset>("Dialogue/" + name).text;
        var data = JsonConvert.DeserializeObject<Dialogue>(loadString);
        dialogues[(int)npcCode] = data;

        Debug.Log("Load Complete " + name + "'s dialogues");
    }
}
