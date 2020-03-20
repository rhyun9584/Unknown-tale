using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClueBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Clue clueData;
    //public bool isObtain = false;

    private Vector2 hotSpot;
    private Texture2D cursor;

    private void Start()
    {
        cursor = GameManager.inst.clueCursor;

        hotSpot.x = cursor.width / 2;
        hotSpot.y = cursor.height / 2;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ClueManager.inst.isObtain[clueData.phonePosition] == false && GameManager.inst.ReturnState() == State.ClueSearch)
        {
            Cursor.SetCursor(cursor, hotSpot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void Obtain()
    {
        if (ClueManager.inst.isObtain[clueData.phonePosition] == false && GameManager.inst.ReturnState() == State.ClueSearch)
        {
            ClueManager.inst.ObtainClue(clueData);

            //ClueManager.inst.isObtain[clueNumber] = true;
        }
    }
}
