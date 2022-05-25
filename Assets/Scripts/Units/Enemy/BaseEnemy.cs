using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    [SerializeField] private Material _baseColor;
    [SerializeField] private Material _highlightedColor;
    [SerializeField] private MeshRenderer _renderer;

    void UpdateRenderer() {
        _renderUpdateNeeded = false;
        if (_isHighlighted) {
            _renderer.material = _highlightedColor;
            return;
        }
        _renderer.material = _baseColor;
    }

    void Start() {
        _renderer.material = _baseColor;
    }

    void Update()
    {
        if (_renderUpdateNeeded) {UpdateRenderer();}
    }

    int EstimatCost(MoveType _moveType) {
        switch (_moveType)
        {
            case MoveType.Rook:
                return 5;
            case MoveType.King:
                return 10;
            case MoveType.Fool:
                return 2;
            case MoveType.ChechFromCenter:
                return 1;
            case MoveType.Knight:
                return 3;
            default:
                return 0;
        }
    }

    public virtual void Move() {
        var moves = GetAllMoves();
        if (moves == null) return;
        Tile move;
        var newMoves = new List<Tile>();
        int best_move = 0;
        foreach (var m in moves)
        {
            if (m._unit != null && m._unit._type == Type.player) {
                if (best_move < EstimatCost(m._unit._moveType)) {
                    best_move = EstimatCost(m._unit._moveType);
                    newMoves = new List<Tile>();
                }
                if (best_move == EstimatCost(m._unit._moveType)) {
                    newMoves.Add(m);
                }
            }
        }
        move = newMoves[Random.Range(0, newMoves.Count)];
        if (move == null) return;
        if (move._unit == null || move._unit != null && move._unit._type != _type) {
            if (!move.Free) {
                Destroy(move._unit.gameObject);
                move._unit = null;
            }
            SetTile(move);
        } 
    }
}
