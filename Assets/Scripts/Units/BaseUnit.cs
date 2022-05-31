using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile _tile;
    [System.NonSerialized] public Type _type;
    public MoveType _moveType;

    public bool dead = false;
    
    public bool _isHighlighted = false;
    public bool _renderUpdateNeeded = false;

    public virtual void Highlighte() {
        _isHighlighted = true; 
        _renderUpdateNeeded = true;
        var moves = GetAllMoves();
        PlayerControlManager.Instance.HighlighteCells(moves);
    }

    public virtual void Unhighlighte() {
        _isHighlighted = false;
        _renderUpdateNeeded = true;
    }

    public bool SetTile(Tile tile) {
        if (tile == null || tile._unit != null) {
            return false;
        }     
        if (_tile != null)
            _tile._unit = null;
        _tile = tile;
        _tile._unit = this;
        return true;
    }

    public bool SnapToTile(Tile tile) {
        if (!SetTile(tile)) {
            return false;
        }     
        transform.position = tile.transform.position;
        transform.rotation = tile.transform.rotation;
        return true;
    }

    public virtual List<Tile> GetAllMoves() {
        return MoveLogic.GetMoves(_type, _moveType, _tile);
    }
}
