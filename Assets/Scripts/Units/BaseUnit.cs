using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile _tile;
    public Type _type;
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
        Debug.Log(this);
        _tile._unit = this;
        Debug.Log(tile._unit);
        return true;
    }

    public bool SnapToTile(Tile tile) {
        if (!SetTile(tile)) {
            return false;
        }     
        transform.position = tile.transform.position;
        transform.rotation = tile.transform.rotation;
        Debug.Log("transformes");
        return true;
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

    static public List<Tile> MovesInDirection(Type _type, Vector2 thisPos, Vector2 dir) {
        var list = new List<Tile>();
        var tile = GridManager.Instance.GetTile(thisPos);
        var stop = false;
        while (tile != null && !stop) {
            if (!tile.Free) {
                if (tile._unit == null) break;
                if (tile._unit._type == _type) break;
                stop = true;
            }
            list.Add(tile);
            thisPos += dir;
            tile = GridManager.Instance.GetTile(thisPos);
        } 
        return list;
    }

    static public List<Tile> RookMoves(Type _type, Tile _tile) {
        if (_tile == null) {
            return null;
        }
        var pos = _tile.GetCoordinates();
        var list = new List<Tile>();
        var directions = Directions.all;
        foreach (Vector2 dir in directions) {
            var thisPos = dir + pos;
            list.AddRange(MovesInDirection(_type, thisPos, dir));
        }
        return list;
    } 

    static public List<Tile> FoolMoves(Type _type, Tile _tile) {
        var pos = _tile.GetCoordinates();
        var list = new List<Tile>();
        var dirs = Directions.all;
        var directions = new List<Vector2>();
        for (var i = 0; i < dirs.Count; i++) {
            directions.Add(dirs[i] + dirs[(i+1)%dirs.Count]);
        }
        foreach (Vector2 dir in directions) {
            var thisPos = dir + pos;
            list.AddRange(MovesInDirection(_type, thisPos, dir));
        }
        return list;
    }

    static public List<Tile> KingMoves(Type _type, Tile _tile) {
        var list = new List<Tile>();
        var pos = _tile.GetCoordinates();

        foreach(var dir in Directions.all) {
            var tile = GridManager.Instance.GetTile(dir+pos);
            if (tile != null && (tile.Free || tile._unit != null && tile._unit._type != _type)) 
                list.Add(tile);
        }
        return list;
    }

    static public List<Tile> KnightMoves(Type _type, Tile _tile) {
        var list = new List<Tile>();
        var pos = _tile.GetCoordinates();

        for (var i = 0; i < 6; i++) {
            var dir = Directions.all[i];
            var tile = GridManager.Instance.GetTile(dir*2+pos +  Directions.all[(i + 1) % 6]);
            if (tile != null && (tile.Free || tile._unit != null && tile._unit._type != _type)) 
                list.Add(tile);
            tile = GridManager.Instance.GetTile(dir*2+pos +  Directions.all[(i + 5) % 6]);
            if (tile != null && (tile.Free || tile._unit != null && tile._unit._type != _type)) 
                list.Add(tile);
        }
        return list;
    }

    static public List<Tile> ChechFromCenterMoves(Type _type, Tile _tile) {
        var list = new List<Tile>();
        var pos = _tile.GetCoordinates();
        int mindist = 100;
        var distances = new Dictionary<Vector2, int>();

        foreach(var dir in Directions.all) {
            var dist = 0;
            var inPos = pos;
            while (GridManager.Instance.GetTile(inPos) != null) {
                inPos += dir;
                dist ++;
            }
            mindist = Mathf.Min(dist, mindist);
            distances[dir] = dist;
        }

        //for ()
        foreach(var dir in Directions.all) {
            if (distances[dir] > mindist) continue;
            var tile = GridManager.Instance.GetTile(dir+pos);
            if (tile != null && tile.Free) 
                list.Add(tile);
            
        }
        return list;
    }

    static public List<Tile> ChechFromEdgeMoves(Type _type, Tile _tile) {
        if (_tile == null) {
            return new List<Tile>();
        }
        var list = new List<Tile>();
        var pos = _tile.GetCoordinates();
        int mindist = 100;
        var distances = new Dictionary<Vector2, int>();

        foreach(var dir in Directions.all) {
            var dist = 0;
            var inPos = pos;
            while (GridManager.Instance.GetTile(inPos) != null) {
                inPos += dir;
                dist ++;
            }
            mindist = Mathf.Min(dist, mindist);
            distances[dir] = dist;
        }

        foreach(var dir in Directions.all) {
            if (distances[dir] > mindist) continue;
            var tile = GridManager.Instance.GetTile(dir+pos);
            if (tile != null && tile.Free) 
                list.Add(tile);
        }
        return list;
    }

    static public List<Tile> GetMoves(Type _type, MoveType _moveType, Tile _fromTile) {
        switch (_moveType)
        {
            case MoveType.Rook:
                return RookMoves(_type, _fromTile);
            case MoveType.King:
                return KingMoves(_type, _fromTile);
            case MoveType.Fool:
                return FoolMoves(_type, _fromTile);
            case MoveType.ChechFromCenter:
                return ChechFromCenterMoves(_type, _fromTile);
            case MoveType.Knight:
                return KnightMoves(_type, _fromTile);
            default:
                return new List<Tile>();
        }
    }

}

