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
    private bool _isHightlited = false;
    private float _hightliteSpeed = 0.1f;
    private Vector3 _hightlitePos = new Vector3(0, 0.3f, 0);
    private Vector3 lerpPosition = new Vector3(0, 0, 0);

    public BaseUnit _unit;

    public bool Free => _unit = null;

    // Start is called before the first frame update
    void Start() {
        _ancor = _transform.position;
    }

    public void InitColor(int pos_x, int pos_y) {
        _color = (new Material[] {_baseColor1, _baseColor2, _baseColor3})[(pos_x+pos_y)%3];
        _renderer.material = _color;
    }

    void Update() {
        var plane = new Plane(Vector3.up, _ancor);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (plane.Raycast(ray, out distance)) {
            Vector3 hitPoint = ray.GetPoint(distance);
            distance = Vector3.Distance(hitPoint, plane.ClosestPointOnPlane(_ancor));
            _isHightlited = (distance < 0.4f);
            lerpPosition = new Vector3(0, 0, 0);
            if (distance < 2f) lerpPosition = (2 - distance)/6 * _hightlitePos;
            if (_isHightlited) {
                lerpPosition = _hightlitePos;
                _renderer.material = _seltectColor;
            }
            else {
                _renderer.material = _color;
            }

        }

        _transform.position = Vector3.Lerp(_transform.position, _ancor + lerpPosition, _hightliteSpeed);
    }
}