using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnut : ScriptableObject
{
    public BaseUnit unitPrefab;
    public Type type;
    public MoveType _moveType;
}

public enum Type {
    player = 0,
    enemy = 1
}

public enum MoveType {
    ChechFromCenter,
    ChechFromEdge,
    Fool,
    Rook,
    Queen,
    King
}

