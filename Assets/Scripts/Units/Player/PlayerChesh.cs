using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChesh : BasePlayer
{
    public override List<Tile> GetAllMoves() {
        var list = new List<Tile>();
        var pos = _tile.GetCoordinates();
        foreach(var dir in Directions.all) {
            var tile = GridManager.Instance.GetTile(dir+pos);
            if (tile != null) 
                list.Add(tile);
        }
        return list;
    }   
}
