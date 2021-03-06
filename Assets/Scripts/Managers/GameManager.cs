﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public Texture2D clueCursor, npcCursor;
    public ReasonManager reasonManager;
    public Camera MainCamera;

    private LocationCode currentLocation;     // 인게임에서 현재 위치
    private State currentState;

    void Awake()
    {
        if (GameManager.inst == null)
            GameManager.inst = this;

        ChangeState(State.NpcSearch);
    }

    void Start()
    {
        ChangeLocation(LocationCode.PartyHall);
    }

    /// <summary>
    /// 플레이어의 맵 이동
    /// </summary>
    public void ChangeLocation(LocationCode nextLocation)
    {
        LocationBase nextLocationScript = LocationManager.inst.locationScript[(int)nextLocation];

        LocationManager.inst.OffLocationUI(currentLocation);

        currentLocation = nextLocation;
        
        LocationManager.inst.CurrentLocationMapping(currentLocation);
        LocationManager.inst.OnLocationUI(currentLocation);

        Debug.Log("Change Location: " + currentLocation);


        // 처음 방문 시 location의 isActive(방문 유무) true
        if (nextLocationScript.GetActive() == false)
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
