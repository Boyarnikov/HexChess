using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{ 
    [SerializeField] private Material _baseColor;
    [SerializeField] private Material _highlightedColor;
    [SerializeField] private Material _usedColor;
    [SerializeField] private MeshRenderer _renderer;
    public int _usedTimes = 0;
    public int _usedTimesLimit = 1;


    void UpdateRenderer() {
        _renderUpdateNeeded = false;
        if (_usedTimes >= _usedTimesLimit) {
            _renderer.material = _usedColor;
            return;
        }
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

    public override List<Tile> GetAllMoves() {
        if (_usedTimes < _usedTimesLimit)
            return Directions.GetMoves(_type, _moveType, _tile);
        return new List<Tile>();
    }   
}
