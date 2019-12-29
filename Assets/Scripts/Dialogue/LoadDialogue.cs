using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

public class LoadDialogue
{
    public static int numOfNpc = 1;

    public static Dialogue[] dialogue = new Dialogue[numOfNpc];

    /// <summary>
    /// Load Dialogue saved as Json
    /// </summary>
    public static void LoadDialogueData()
    {
        for(int i = 0; i < numOfNpc; i++)
        {
            string loadString = Resources.Load<TextAsset>("Dialogue/sample1").text;
            var data = JsonConvert.DeserializeObject<Dialogue>(loadString);
            dialogue[i] = data;
        }
    }
}
