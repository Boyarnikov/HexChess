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
        var figures = (BasePlayer[])FindObjectsOfType(typeof(BasePlayer));
        foreach (var fig in figures)
        {
            fig._usedTimes = 0;
            fig._renderUpdateNeeded = true;
        }
    }

    void LeftMouseDown() {
        UnhighlighteCells();
        if (GameManager.Instance.GetState() != GameState.AwaitMove) 
            return;
        
        if (hightlighted != null) 
        {
            if (lastSelected != null && lastSelected._unit != null &&
                    lastSelected._unit._type == Type.player) {
                var playerFigure = (BasePlayer)lastSelected._unit;
                if (playerFigure._usedTimes < playerFigure._usedTimesLimit
                        && playerFigure.GetAllMoves().Contains(hightlighted)) {
                    if (!hightlighted.Free) {
                        var u = hightlighted._unit.gameObject;
                        hightlighted._unit._tile = null;
                        hightlighted._unit = null;
                        Destroy(u);
                    }
                    playerFigure.SetTile(hightlighted);
                    energy--;
                    playerFigure._usedTimes++;
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

        if (energy <= 0) {
            if (lastSelected != null)
                lastSelected.Unselect();
            lastSelected = null;
            UnhighlighteCells();
            GameManager.Instance.ChangeState(GameState.MoveEnemies);
        }
    }

    void RightMouseDown() {
        UnhighlighteCells();
        if (lastSelected != null) {
            lastSelected.Unselect();
            lastSelected = null;
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) 
            LeftMouseDown(); 
        if (Input.GetMouseButtonDown(1)) 
            RightMouseDown();
    }
}

