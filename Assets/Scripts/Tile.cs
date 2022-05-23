using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Материалы для пресета и рендерер меша
    [SerializeField] private Material _baseColor1;
    [SerializeField] private Material _baseColor2;
    [SerializeField] private Material _baseColor3;
    [SerializeField] private Material _inspectColor;
    [SerializeField] private Material _seltectColor;
    [SerializeField] private Material _deactivatedColor;
    [SerializeField] private MeshRenderer _renderer;

    private Material _color;                // Базовый цвет клетки

    private Vector3 _ancor;                 // Якорь клетки в глобальный координатах
    private Vector2 _coordinates;           // Координаты клетки в системе поля
    private bool _isHightlited = false;     // Наведена ли мышка
    private bool _isActive = true;          // Активна ли ячейка


    private Vector3 _lerpPosition = new Vector3(0, 0, 0);
    private Quaternion _lerpRotation = Quaternion.identity;
    
    private Quaternion _deactivatedRot = Quaternion.Euler(new Vector3(180, 0, 0));
    private Vector3 _hightlitedPos = new Vector3(0, 0.3f, 0);
    private Vector3 _deactivatedPos = new Vector3(0, -0.3f, 0);
    
    private const float _lerpSpeed = 0.1f;

    public BaseUnit _unit = null;

    public bool Free => _unit == null;

    void Start() {
        _ancor = transform.position;
    }

    public void Init(int pos_x, int pos_y) {
        _coordinates = new Vector2(pos_x, pos_y);
        _color = (new Material[] {_baseColor1, _baseColor2, _baseColor3})[(pos_x+pos_y)%3];
        _renderer.material = _color;
    }

    void UpdatePosition() {
        transform.position = Vector3.Lerp(transform.position,
             _ancor + _lerpPosition, _lerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, _lerpRotation, _lerpSpeed);
        if (_unit != null) {
            _unit.transform.position = Vector3.Lerp(_unit.transform.position,
                _ancor + 2 * _lerpPosition, _lerpSpeed);
            _unit.transform.rotation = transform.rotation;
            }
    }   

    void OnMouseDown() {
        if (GameManager.Instance.GetState() != GameState.AwaitMove) 
            return;
        _isActive = false;
    }

     void UpdateRenderer() {
        if (!_isActive) {
            _renderer.material = _deactivatedColor;
            return;
        }
        if (_isHightlited) {
            _renderer.material = _seltectColor;
            return;
        }
        _renderer.material = _color;
    }


    void CalculateLerp() {
        if (!_isActive) {
            _lerpPosition = _deactivatedPos;
            _lerpRotation = _deactivatedRot;
            _isHightlited = false;
            return;
        }

        _lerpRotation = Quaternion.identity;
        float distance = Vector3.Distance(GridManager.mousePos, _ancor);
        _isHightlited = (distance < 0.4f);
        _lerpPosition = new Vector3(0, 0, 0);
        if (distance < 2f) _lerpPosition = (2 - distance)/6 * _hightlitedPos;
        if (_isHightlited) {
            _lerpPosition = _hightlitedPos;
        }

    }

    void Update() {
        CalculateLerp();
        UpdateRenderer();
        UpdatePosition();
    }
}