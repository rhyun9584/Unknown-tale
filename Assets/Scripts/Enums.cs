using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCCode
{
    MAIN,
    SAMPLE1,
    SAMPLE2,
    SAMPLE3,
    OceanKing,  // 4 용왕
    Octopus,    // 5 문어 대왕
    OceanSon,   // 6 용왕 아들
    Angler,     // 7 아귀 대신
    Anchovy     // 8 멸치 시종
}

public enum State
{
    ClueSearch,
    NpcSearch,
    Talk,
    Map,
    Phone,
    Character,  // Phone UI 내부 Character UI를 켠 state
    Clue        // Phone UI 내부 Clue UI를 켠 state
}

public enum LocationCode
{
    LOCATION1,
    LOCATION2,
    PartyHall
}
