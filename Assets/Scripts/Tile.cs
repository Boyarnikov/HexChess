using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Материалы для пресета и рендерер меша
    [SerializeField] private Material _baseColor1;
    [SerializeField] private Material _baseColor2;
    [SerializeField] private Material _baseColor3;
    [SerializeField] private Material _hightlightedColor;
    [SerializeField] private Material _selectedColor;
    [SerializeField] private Material _deactivatedColor;
    [SerializeField] private MeshRenderer _renderer;

    private Material _color;                // Базовый цвет клетки

    private Vector3 _ancor;                 // Якорь клетки в глобальный координатах
    private Vector2 _coordinates;           // Координаты клетки в системе поля
    private bool _isSelected = false; 
    private bool _isHighlighted = false;     // Наведена ли мышка
    private bool _isActive = true;          // Активна ли ячейка

    // Позиции для лерпа
    private Vector3 _lerpPosition = new Vector3(0, 0, 0);
    private Quaternion _lerpRotation = Quaternion.identity;
    
    // Константы для лерпа
    private Quaternion _deactivatedRot = Quaternion.Euler(new Vector3(180, 0, 0));
    private Vector3 _highlightedPos = new Vector3(0, 0.3f, 0);
    private Vector3 _deactivatedPos = new Vector3(0, -0.3f, 0);
    private const float _lerpSpeed = 0.1f;

    // Мета про клетку
    public BaseUnit _unit = null;

    public bool Free => _unit == null && _isActive;

    public void Highlighte() {
        _isHighlighted = true; 
        if (_unit != null) {
            _unit.Highlighte();
        }
    }
    public void Unhighlighte() {
        _isHighlighted = false;
        if (_unit != null) {
            _unit.Unhighlighte();
        }
    }

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
                _ancor + 1.3f * _lerpPosition, _lerpSpeed);
            _unit.transform.rotation = transform.rotation;
            }
    }   

    void MouseDown() {
        if (GameManager.Instance.GetState() != GameState.AwaitMove) 
            return;
        if (!_isActive) 
            return;
        if (_isSelected) {
            Highlighte();
            return;
        }
        Unhighlighte();
    }

    public void Deactivate() {
        _isActive = false;
    }

     void UpdateRenderer() {
        if (!_isActive) {
            _renderer.material = _deactivatedColor;
            return;
        }
        if (_isHighlighted) {
            _renderer.material = _hightlightedColor;
            return;
        }
        if (_isSelected) {
            _renderer.material = _selectedColor;
            return;
        }
        _renderer.material = _color;
    }


    void CalculateLerp() {
        if (!_isActive) {
            _lerpPosition = _deactivatedPos;
            _lerpRotation = _deactivatedRot;
            _isSelected = false;
            return;
        }

        _lerpRotation = Quaternion.identity;
        float distance = Vector3.Distance(GridManager.mousePos, _ancor);
        _isSelected = (distance < 0.4f);
        _lerpPosition = new Vector3(0, 0, 0);
        if (distance < 2f) _lerpPosition = (2 - distance)/6 * _highlightedPos;
        if (_isSelected) {
            _lerpPosition = _highlightedPos;
        }
        if (_isHighlighted) {
            _lerpPosition = 2 * _highlightedPos;
        }

    }

    void Update() {
        if (Input.GetMouseButton(0)) 
            MouseDown();
        CalculateLerp();
        UpdateRenderer();
        UpdatePosition();
    }
}