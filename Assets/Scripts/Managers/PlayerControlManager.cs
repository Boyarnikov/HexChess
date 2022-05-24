using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    public static PlayerControlManager Instance;
    public Tile hightlighted;
    public Tile lastSelected;
    public int energy = 0;
    public int maxEnergy = 3;

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

    public void StartTurn() {
        energy = maxEnergy;
    }

    void MouseDown() {
        UnhighlighteCells();
        if (GameManager.Instance.GetState() != GameState.AwaitMove) 
            return;
        
        if (hightlighted != null) 
        {
            if (lastSelected != null && lastSelected._unit != null &&
                    lastSelected._unit._type == Type.player) {
                var playerFigure = lastSelected._unit;
                if (playerFigure.GetAllMoves().Contains(hightlighted)) {
                    if (!hightlighted.Free) {
                        Destroy(hightlighted._unit.gameObject);
                        hightlighted._unit = null;
                    }
                    playerFigure.SetTile(hightlighted);
                    energy--;
                }
            } 
        }
        if (lastSelected != null) {
            lastSelected.Unselect();
            lastSelected = null;
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
        if (energy <= 0) {
            GameManager.Instance.ChangeState(GameState.SpawnEnemies);
        }
    }
}

