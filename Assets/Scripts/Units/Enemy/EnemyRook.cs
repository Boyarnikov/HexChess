using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRook : BaseEnemy
{
    public override List<Tile> GetAllMoves() {
        if (_tile == null) {
            return null;
        }
        var pos = _tile.GetCoordinates();
        var list = new List<Tile>();
        var directions = Directions.all;
        Tile tile = null;
        bool stop = false;
        foreach (Vector2 dir in directions) {
            var thisPos = dir + pos;
            tile = GridManager.Instance.GetTile(thisPos);
            stop = false;
            while (tile != null && !stop) {
                if (!tile.Free) {
                    if (tile._unit._type == _type) break;
                    stop = true;
                }
                list.Add(tile);
                thisPos += dir;
                tile = GridManager.Instance.GetTile(thisPos);
            } 
        }
        return list;
    }   
}
