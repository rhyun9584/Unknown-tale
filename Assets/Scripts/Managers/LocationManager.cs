using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager inst;

    public static int locationCount = System.Enum.GetValues(typeof(LocationCode)).Length;

    [Tooltip("LocationCode 순서대로 Scene에서 매칭")]
    public GameObject[] location;

    [HideInInspector]
    public LocationBase[] locationScript;

    private GameObject currentLocation, currentNPC, currentClue;

    private void Awake()
    {
        inst = this;

        locationScript = new LocationBase[locationCount];
        for(int i = 0; i < locationCount; i++)
        {
            locationScript[i] = location[i].GetComponent<LocationBase>();
        }
    }

    public void OnLocationUI(LocationCode locationCode)
    {
        location[(int)locationCode].SetActive(true);
        SearchUIChange();
    }

    public void OffLocationUI(LocationCode locationCode)
    {
        location[(int)locationCode].SetActive(false);
    }

    /// <summary>
    /// Location이 변경되면 해당 location에 맞는 gameobject 매핑
    /// </summary>
    /// <param name="locationCode"></param>
    public void CurrentLocationMapping(LocationCode locationCode)
    {
        currentLocation = location[(int)locationCode];

        currentNPC = currentLocation.transform.Find("npc").gameObject;
        currentClue = currentLocation.transform.Find("clue").gameObject;
    }

    /// <summary>
    /// 화면에 npc 혹은 clue가 state에 맞게 끄고 켬
    /// </summary>
    public void SearchUIChange()
    {
        State currentState = GameManager.inst.ReturnState();

        // 현장수사로 전환
        if(currentState == State.ClueSearch)
        {
            currentNPC.SetActive(false);
            currentClue.SetActive(true);
        }
        else if(currentState == State.NpcSearch)
        {
            currentNPC.SetActive(true);
            currentClue.SetActive(false);
        }
    }
}
