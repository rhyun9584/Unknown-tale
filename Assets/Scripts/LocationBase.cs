using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationBase : MonoBehaviour
{
    [SerializeField]
    private LocationCode locationCode;
    [SerializeField]
    private string locationName;
    [SerializeField]
    private int viewCount; // 해당 맵 속 시점의 총 수

    // 한 번이라도 활성화된 적 있는지
    private bool isActive;

    public void Awake()
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

    public int GetViewCount()
    {
        return viewCount;
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
