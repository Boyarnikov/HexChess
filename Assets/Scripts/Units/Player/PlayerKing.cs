using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKing : BasePlayer
{
    public override List<Tile> GetAllMoves() {
        return Directions.GetMoves(_type, _moveType, _tile);
    }  
}
