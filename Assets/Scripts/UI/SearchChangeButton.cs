using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchChangeButton : MonoBehaviour
{
    public void ChangeSearchState()
    {
        State currentState = GameManager.inst.ReturnState();

        if (currentState == State.NpcSearch)
        {
            GameManager.inst.ChangeState(State.ClueSearch);
            LocationManager.inst.SearchUIChange();
        }
        else if(currentState == State.ClueSearch)
        {
            GameManager.inst.ChangeState(State.NpcSearch);
            LocationManager.inst.SearchUIChange();
        }
    }
}
