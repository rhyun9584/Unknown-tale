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
    /// Map UI에서 사용하는 이동 버튼
    /// </summary>
    public void MoveLocationInMap()
    {
        // Debug.Log(GameManager.inst.ReturnState() + " " + LocationManager.inst.locationScript[(int)nextLocation].GetActive()
        //     + " " + LocationManager.inst.location[(int)nextLocation].GetComponent<LocationBase>().GetActive());
        if(GameManager.inst.ReturnLocation() == nextLocation) // current Location == next Location
        {
            Debug.Log(nextLocation.ToString() + " is here");
        }
        else if(GameManager.inst.ReturnState() == State.Map && LocationManager.inst.locationScript[(int)nextLocation].GetActive() == true)
        {
            GameManager.inst.ChangeLocation(nextLocation);
            //MapUI.inst.CloseMapUI();
            PhoneUI.inst.ClosePhoneUI();
        }
        else // next Location is not activated 한 번도 방문한 적 없는 장소로 이동 시에
        {
            Debug.Log(nextLocation.ToString() + " is not activated");
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

    /// <summary>
    /// Map UI의 Simple UI를 켜는 경우 moveLocationButton의 nextLocation을 수정
    /// State가 Map인 경우만 동작하도록 설정하여 다른 상황에서 이용 불가
    /// </summary>
    public void ChangeNextLocation(LocationCode locationCode)
    {
        if(GameManager.inst.ReturnState() == State.Map)
        {
            this.nextLocation = locationCode;
        }
    }
}
