using UnityEngine;
using System.Collections;

public enum FoxActionType { Walk, Jump, Stand}

[System.Serializable]
public class FoxAction
{
    public FoxActionType type;
    public float time;
    public AnimationCurve curveA;
    public AnimationCurve curveB;
}