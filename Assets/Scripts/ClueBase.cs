using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBase : MonoBehaviour
{
    public string clueName;
    public int clueNumber;

    //public bool isObtain = false;

    public void Obtain()
    {
        if (ClueManager.inst.isObtain[clueNumber] == false)
        {
            ClueManager.inst.ObtainClue(clueNumber, clueName);

            //ClueManager.inst.isObtain[clueNumber] = true;
        }
    }
}
