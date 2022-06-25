using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptUI : UI {
    [SerializeField]
    Character talker;
}

public struct Character {
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private string characterName;
}