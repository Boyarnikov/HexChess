using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnut : ScriptableObject
{
    public BaseUnit UnitPrefab;
    public Type type;
}

public enum Type {
    player = 0,
    enemy = 1
}
