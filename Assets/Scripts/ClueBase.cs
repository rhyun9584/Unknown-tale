using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBase : MonoBehaviour
{
    public string clueName;

    private bool isObtain = false;

    public void Obtain()
    {
        if (!isObtain)
        {
            isObtain = true;

            ClueManager.inst.ObtainClue(this);
        }
    }
}
