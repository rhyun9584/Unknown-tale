using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewChangeButton : MonoBehaviour
{
    public int nextViewNum;

    public void ChangeView()
    {
        LocationManager.inst.SetView(nextViewNum);
    }
}
