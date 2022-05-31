using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{ 
    private Material _baseColor;
    private Material _highlightedColor;
    private Material _usedColor;
    private MeshRenderer _renderer;

    public int _usedTimes = 0;
    public int _usedTimesLimit = 1;

    void Start() {
        _baseColor = UnitManager.Instance._playerBaseColor;
        _highlightedColor = UnitManager.Instance._playerHighlightedColor;
        _usedColor = UnitManager.Instance._playerUsedColor;
        _renderer = transform.Find("Mesh").gameObject.GetComponent<MeshRenderer>();
        _renderer.material = _baseColor;
        _type = Type.player;
    }

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

    void Update()
    {
        if (_renderUpdateNeeded) {UpdateRenderer();}
    }

    public override List<Tile> GetAllMoves() {
        if (_usedTimes < _usedTimesLimit)
            return MoveLogic.GetMoves(_type, _moveType, _tile);
        return new List<Tile>();
    }   
}
