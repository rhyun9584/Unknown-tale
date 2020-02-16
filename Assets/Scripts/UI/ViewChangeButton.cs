using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewChangeButton : MonoBehaviour
{
    public int nextViewNum;

    public void ChangeView()
    {
        State currentState = GameManager.inst.ReturnState();

        if(currentState == State.ClueSearch || currentState == State.NpcSearch)
        {
            LocationManager.inst.SetView(nextViewNum);
        }
    }
}
