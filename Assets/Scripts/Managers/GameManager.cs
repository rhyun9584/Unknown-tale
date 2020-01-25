using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    private LocationCode currentLocation;     // 인게임에서 현재 위치
    private State currentState;

    void Awake()
    {
        if (GameManager.inst == null)
            GameManager.inst = this;
    }

    void Start()
    {
        ChangeLocation(LocationCode.LOCATION1);
        ChangeState(State.Search);

    }

    /// <summary>
    /// 플레이어의 맵 이동
    /// </summary>
    public void ChangeLocation(LocationCode nextLocation)
    {
        LocationBase nextLocationScript = LocationManager.inst.locationScript[(int)nextLocation];

        LocationManager.inst.OffLocationUI(currentLocation);
        LocationManager.inst.OnLocationUI(nextLocation);

        currentLocation = nextLocation;

        Debug.Log("Change Location: " + currentLocation);
        
        // 처음 방문 시 location의 isActive(방문 유무) true
        if(nextLocationScript.GetActive() == false)
        {
            nextLocationScript.SetActive();
        }   

    }

    public void ChangeState(State nextState)
    {
        currentState = nextState;

        Debug.Log("Change State: " + currentState);
    }

    /// <summary>
    /// ReturnState를 private 상태에서 값을 받아오기 위함
    /// </summary>
    /// <returns></returns>
    public State ReturnState()
    {
        return currentState;
    }
    
    public LocationCode ReturnLocation()
    {
        return currentLocation;
    }
}
