using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoveButton : MonoBehaviour
{
    public MapCode nextMap;
    public void MoveMap()
    {
        // 대화 진행 중 이동 불가
        if(GameManager.inst.ReturnState() != State.Talk)
        GameManager.inst.ChangeLocation(nextMap);
    }
}
