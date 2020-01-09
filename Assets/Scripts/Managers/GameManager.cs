using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    private LocationCode currentLocation;     // 인게임에서 현재 위치
    private State currentState;

    public static int locationCount = System.Enum.GetValues(typeof(LocationCode)).Length;
    public static int npcCount = System.Enum.GetValues(typeof(NPCCode)).Length;

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
        LocationManager.inst.OffLocationUI(currentLocation);

        currentLocation = nextLocation;
        LocationBase currentLocationScript = LocationManager.inst.locationScript[(int)currentLocation];

        Debug.Log("Change Location: " + currentLocation);

        LocationManager.inst.OnLocationUI(currentLocation);
        
        if(currentLocationScript.GetActive() == false)
        {
            currentLocationScript.SetActive();
        }

    }

    public void ChangeState(State nextState)
    {
        currentState = nextState;

        Debug.Log("Change State: " + currentState);

        // 각 State에 맞는 UI or 동작제어 <- UI Manager function 호출
    }

    /// <summary>
    /// ReturnState를 private 상태에서 값을 받아오기 위함
    /// </summary>
    /// <returns></returns>
    public State ReturnState()
    {
        return currentState;
    }
}
