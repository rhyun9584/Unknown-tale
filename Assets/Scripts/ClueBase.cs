using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBase : MonoBehaviour
{
    public string clueName;
    public int clueNumber;

    public bool isObtain = false;

    public void Obtain()
    {
        if (!isObtain)
        {
            ClueManager.inst.ObtainClue(clueNumber, clueName);
            
            isObtain = true;
        }
    }
}
