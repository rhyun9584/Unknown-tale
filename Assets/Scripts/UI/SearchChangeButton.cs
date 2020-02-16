using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchChangeButton : MonoBehaviour
{
    private Image iconImage;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
    }

    public void ChangeSearchState()
    {
        State currentState = GameManager.inst.ReturnState();

        if (currentState == State.NpcSearch)
        {
            GameManager.inst.ChangeState(State.ClueSearch);
            LocationManager.inst.SearchUIChange();
            iconImage.sprite = Resources.Load<Sprite>("UI/Icon/Field investigation");
        }
        else if(currentState == State.ClueSearch)
        {
            GameManager.inst.ChangeState(State.NpcSearch);
            LocationManager.inst.SearchUIChange();
            iconImage.sprite = Resources.Load<Sprite>("UI/Icon/Legwork icon");
        }
    }
}
