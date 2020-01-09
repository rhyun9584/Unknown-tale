using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager inst;

    [Tooltip("LocationCode 순서대로 Scene에서 매칭")]
    public GameObject[] location;

    [HideInInspector]
    public LocationBase[] locationScript;

    private void Awake()
    {
        inst = this;

        locationScript = new LocationBase[GameManager.locationCount];
        for(int i = 0; i < GameManager.locationCount; i++)
        {
            locationScript[i] = location[i].GetComponent<LocationBase>();
        }
    }

    public void OnLocationUI(LocationCode locationCode)
    {
        location[(int)locationCode].SetActive(true);
    }

    public void OffLocationUI(LocationCode locationCode)
    {
        location[(int)locationCode].SetActive(false);
    }

}
