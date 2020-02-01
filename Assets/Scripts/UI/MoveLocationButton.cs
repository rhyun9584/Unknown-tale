using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocationButton : MonoBehaviour
{
    [SerializeField]
    private LocationCode nextLocation;

    /// <summary>
    /// 인게임에서 장소 이동 버튼(ex 문)
    /// </summary>
    public void MoveLocation()
    {
        // 조사 상태에서만 이동 가능
        if(GameManager.inst.ReturnState() == State.Search)
        {
            GameManager.inst.ChangeLocation(nextLocation);
        }
    }

    /// <summary>
    /// Map UI 내 Location Button의 동작을 담당하는 함수
    /// simple UI를 켬
    /// </summary>
    public void OpenSimpleUI()
    {
        MapUI.inst.OnSimpleUI(nextLocation);
    }

}
