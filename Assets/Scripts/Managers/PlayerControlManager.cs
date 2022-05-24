using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    public static PlayerControlManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void HighlighteCells(List<Tile> tiles) {
        foreach (var tile in tiles)
        {
            tile.Attack();
        }
    }

    public void UnhighlighteCells() {
        var tiles = GridManager.Instance.GetAllTiles();
        foreach (var tile in tiles) {
            tile.Unattack();
        }
    }
}

