using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationBase : MonoBehaviour
{
    [SerializeField]
    private LocationCode locationCode;
    [SerializeField]
    private string locationName;

    private bool isActive;

    private void Start()
    {
        isActive = false;
    }

    public bool GetActive()
    {
        return isActive;
    }

    /// <summary>
    /// isActive False -> True
    /// </summary>
    public void SetActive()
    {
        isActive = true;
    }

    public LocationCode GetLocationCode()
    {
        return locationCode;
    }

    public string GetLocationName()
    {
        return locationName;
    }
}
