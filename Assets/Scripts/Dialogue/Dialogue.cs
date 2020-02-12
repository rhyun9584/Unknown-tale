using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public int maxState;
    public Talks[] talks;
}

public class Talks
{
    public string portrait;
    public int npccode;
    public int face;
    public string speaker;
    public string sentence;
}
