using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocationButton : MonoBehaviour
{
    [SerializeField]
    private LocationCode nextLocation;

    public void MoveLocation()
    {
        // 조사 상태에서만 이동 가능
        if(GameManager.inst.ReturnState() == State.Search)
        {
            GameManager.inst.ChangeLocation(nextLocation);
        }
    }

    public void MoveLocationInMap()
    {
        // Debug.Log(GameManager.inst.ReturnState() + " " + LocationManager.inst.locationScript[(int)nextLocation].GetActive()
        //     + " " + LocationManager.inst.location[(int)nextLocation].GetComponent<LocationBase>().GetActive());
        if(GameManager.inst.ReturnLocation() == nextLocation)
        {
            Debug.Log(nextLocation.ToString() + " is here");
        }
        else if(GameManager.inst.ReturnState() == State.Map && LocationManager.inst.locationScript[(int)nextLocation].GetActive() == true)
        {
            GameManager.inst.ChangeLocation(nextLocation);
            MapUI.inst.CloseMapUI();
        }
        else
        {
            Debug.Log(nextLocation.ToString() + " is not activated");
        }
    }
}
