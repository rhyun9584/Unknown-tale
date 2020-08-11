using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCCode
{
    MAIN,
    OceanKing,      // 1 용왕
    Octopus,        // 2 문어 대왕
    OceanSon,       // 3 용왕 아들
    Angler,         // 4 아귀 대신
    Anchovy,        // 5 멸치 시종
    MountainKing    // 6 산왕
}

public enum State
{
    ClueSearch,
    NpcSearch,
    Talk,
    Map,
    Phone,
    Character,  // Phone UI 내부 Character UI를 켠 state
    Clue,       // Phone UI 내부 Clue UI를 켠 state
    Reasoning   // 추리 시작 버튼을 눌러 시작한 state
}

public enum LocationCode
{
    PartyHall
}
