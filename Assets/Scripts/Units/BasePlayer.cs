using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{
    private bool _isHighlighted = false;
    private bool _renderUpdateNeeded = false;
    
    [SerializeField] private Material _baseColor;
    [SerializeField] private Material _highlightedColor;
    [SerializeField] private MeshRenderer _renderer;

    public override void Highlighte() {
        _isHighlighted = true; 
        _renderUpdateNeeded = true;
        var moves = GetAllMoves();
        PlayerControlManager.Instance.HighlighteCells(moves);
    }

    public override void Unhighlighte() {
        _isHighlighted = false;
        _renderUpdateNeeded = true;
        PlayerControlManager.Instance.UnhighlighteCells();
    }

    void UpdateRenderer() {
        _renderUpdateNeeded = false;
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
}
