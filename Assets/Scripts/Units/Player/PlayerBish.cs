using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBish : BasePlayer
{
        public override List<Tile> GetAllMoves() {
        var pos = _tile.GetCoordinates();
        var list = new List<Tile>();
        var dirs = Directions.all;
        var directions = new List<Vector2>();
        for (var i = 0; i < dirs.Count; i++) {
            directions.Add(dirs[i] + dirs[(i+1)%dirs.Count]);
        }
        Tile tile = null;
        bool stop = false;
        foreach (Vector2 dir in directions) {
            var thisPos = dir + pos;
            tile = GridManager.Instance.GetTile(thisPos);
            stop = false;
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
        }
        return list;
    }   
}
