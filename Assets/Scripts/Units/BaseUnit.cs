using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile _tile;
    public Type _type;
    
    public virtual void Highlighte() {
    }
    public virtual void Unhighlighte() {
    }

    public void SetTile(Tile tile) {
        if (tile == null || tile._unit != null) {
            return;
        }     
        if (_tile != null)
            _tile._unit = null;
        _tile = tile;
        _tile._unit = this;
    }

    public virtual List<Tile> GetAllMoves() {
        return null;
    }   
}

public class Directions {
    static public List<Vector2> All() {
        List<Vector2> directions = new List<Vector2>();
        directions.Add(Vector2.down);
        directions.Add(Vector2.left);
        directions.Add(Vector2.right);
        directions.Add(Vector2.up);
        directions.Add(Vector2.down+Vector2.left);
        directions.Add(Vector2.right+Vector2.up);
        return directions;
    }
}
 