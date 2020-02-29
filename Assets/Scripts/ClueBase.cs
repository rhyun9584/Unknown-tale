using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBase : MonoBehaviour
{
    public Clue clueData;
    //public bool isObtain = false;

    public void Obtain()
    {
        if (ClueManager.inst.isObtain[clueData.phonePosition] == false && GameManager.inst.ReturnState() == State.ClueSearch)
        {
            ClueManager.inst.ObtainClue(clueData);

            //ClueManager.inst.isObtain[clueNumber] = true;
        }
    }
}
