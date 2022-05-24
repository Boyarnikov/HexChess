using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile _tile;
    public Type _type;
    
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

    public void SetTile(Tile tile) {
        if (tile == null || tile._unit != null) {
            return;
        }     
        if (_tile != null)
            _tile._unit = null;
        _tile = tile;
        Debug.Log(this);
        _tile._unit = this;
        Debug.Log(tile._unit);
    }

    public virtual List<Tile> GetAllMoves() {
        return null;
    }   
}

public static class Directions {
    public static List<Vector2> all = All();

    static public List<Vector2> All() {
        List<Vector2> directions = new List<Vector2>();
        directions.Add(Vector2.down);
        directions.Add(Vector2.down+Vector2.left);
        directions.Add(Vector2.left);
        directions.Add(Vector2.up);
        directions.Add(Vector2.right+Vector2.up);
        directions.Add(Vector2.right);
        return directions;
    }
}