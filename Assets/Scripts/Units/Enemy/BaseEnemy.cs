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

    public virtual void Move() {
        var moves = GetAllMoves();
        if (moves == null) return;
        var move = moves[Random.Range(0, moves.Count)];
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
