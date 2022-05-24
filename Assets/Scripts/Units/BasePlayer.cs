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
    }
    public override void Unhighlighte() {
        _isHighlighted = false;
        _renderUpdateNeeded = true;
        }

    void UpdateRenderer() {
        if (_isHighlighted) {
            _renderer.material = _highlightedColor;
            return;
        }
        _renderer.material = _baseColor;
    }

    void Update()
    {
        if (_renderUpdateNeeded) {
            UpdateRenderer();
        }
    }
}
