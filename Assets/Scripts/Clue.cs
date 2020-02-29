using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clue", menuName = "ClueData")]
public class Clue : ScriptableObject
{
    public string code;
    public new string name;
    [TextArea]
    public string explain;

    public int phonePosition;

    [TextArea]
    public string[] obtainScript;

    public Sprite clueImage;

}
