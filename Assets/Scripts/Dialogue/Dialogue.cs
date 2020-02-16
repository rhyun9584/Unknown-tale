using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public int maxState;
    public Talks[][] talks; // 첫 번째는 state, 두 번째는 한 state에서 이어지는 대화
}

public class Talks
{
    public string portrait;
    public int npccode;
    public int face;
    public string speaker;
    public string sentence;
}
