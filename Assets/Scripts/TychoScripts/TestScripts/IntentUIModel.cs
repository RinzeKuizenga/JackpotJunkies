using UnityEngine;
using System;

public enum IntentType
{
    Attack,
    Defend,
    Buff,
    Debuff,
    Special,
    Unknown
}

[Serializable]
public struct IntentUIModel {
    public IntentType type;
    public int value;
    public string title;
    public string description;
    public bool isHidden;
}
