using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBase : MonoBehaviour
{
    public string clueName;
    public int clueNumber;

    private bool isObtain = false;

    public void Obtain()
    {
        if (!isObtain)
        {
            isObtain = true;

            ClueManager.inst.ObtainClue(clueNumber, clueName);
        }
    }
}
