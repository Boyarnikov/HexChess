using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private Material _baseColor1, _baseColor2, _baseColor3, _inspectColor, _seltectColor;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private GameObject _hightliter;

    private Material _color;

    private Vector3 _ancor;
    private Vector2 _coordinates;
    private bool _isHightlited = false;
    private bool _isActive = true;
    private Vector3 _lerpPosition = new Vector3(0, 0, 0);
    
    private Vector3 _hightlitedPos = new Vector3(0, 0.3f, 0);
    private Vector3 _deactivatedPos = new Vector3(0, -10f, -20);
    
    private const float _hightliteSpeed = 0.1f;

    public BaseUnit _unit;

    public bool Free => _unit = null;

    void Start() {
        _ancor = _transform.position;
        _transform.position += _deactivatedPos;
    }

    public void Init(int pos_x, int pos_y) {
        _coordinates = new Vector2(pos_x, pos_y);
        _color = (new Material[] {_baseColor1, _baseColor2, _baseColor3})[(pos_x+pos_y)%3];
        _renderer.material = _color;
    }

    void UpdatePosition() {
        _transform.position = Vector3.Lerp(_transform.position,
             _ancor + _lerpPosition, _hightliteSpeed);
        if (_unit != null) 
            _unit.transform.position = Vector3.Lerp(_unit.transform.position,
                _ancor + 2 * _lerpPosition, _hightliteSpeed);
    }   

    void private void OnMouseDown() {
        if (GameManager.Instance._gamestate != GameState.AwaitMove) 
            reutrn;
    }

    void CalculateLerp() {
        if (!_isActive) {
            _lerpPosition = _ancor + _deactivatedPos;
            _isHightlited = false;
            return;
        }

        float distance = Vector3.Distance(GridManager.mousePos, _ancor);
        _isHightlited = (distance < 0.4f);
        _lerpPosition = new Vector3(0, 0, 0);
        if (distance < 2f) _lerpPosition = (2 - distance)/6 * _hightlitedPos;
        if (_isHightlited) {
            _lerpPosition = _hightlitedPos;
            _renderer.material = _seltectColor;
        }
        else {
            _renderer.material = _color;
        }
    }

    void Update() {
        CalculateLerp();
        UpdatePosition();
    }
}