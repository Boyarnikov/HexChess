using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChesh : BasePlayer
{
    public override List<Tile> GetAllMoves() {
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
}
