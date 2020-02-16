using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryImage : MonoBehaviour
{
    public Sprite[] tempoSprite = new Sprite[4];

    private int checkSprite = 0;
    
     

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gameObject.GetComponent<Image>().sprite = tempoSprite[checkSprite++];
    }
}
