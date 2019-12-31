using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoveButton : MonoBehaviour
{
    public MapCode nextMap;
    public void MoveMap()
    {
        GameManager.inst.ChangeLocation(nextMap);
    }
}
