using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocationButton : MonoBehaviour
{
    [SerializeField]
    private MapCode nextLocation;

    public void MoveLocation()
    {
        // 대화 진행 중 이동 불가
        if(GameManager.inst.ReturnState() != State.Talk)
        GameManager.inst.ChangeLocation(nextLocation);
    }
}
