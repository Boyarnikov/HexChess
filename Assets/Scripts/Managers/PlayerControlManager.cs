using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    public static PlayerControlManager Instance;
    public Tile hightlighted;
    public Tile lastSelected;

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

    void MouseDown() {
        UnhighlighteCells();
        if (GameManager.Instance.GetState() != GameState.AwaitMove) 
            return;
        if (lastSelected != null) {
            lastSelected.Unselect();
        }
        if (hightlighted != null) 
        {
            hightlighted.Select();
            lastSelected = hightlighted;
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) 
            MouseDown();
    }
}

