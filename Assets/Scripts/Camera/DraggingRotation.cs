using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingRotation : MonoBehaviour
{
    Vector3 _from;
    Vector3 _rotPoint;
    Vector3 _pos;
    Quaternion _rotation;

    float _rotateMove;


    void Start()
    {
        _rotPoint = GridManager.Instance.GetMid();
        _pos = transform.position - _rotPoint;
        _rotation = transform.rotation;
        _rotateMove = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
            _from = Input.mousePosition;
            _pos = transform.position - _rotPoint;
            _rotation = transform.rotation;
            _rotateMove = 0;
        }
        if (Input.GetMouseButton(0)) {
            var amount = (Input.mousePosition.x -_from.x)/4;
            _rotateMove = (float) (_rotateMove  * 0.9 + amount  * 0.1);
            transform.position = Quaternion.AngleAxis(_rotateMove, Vector3.up) * _pos + _rotPoint;
            transform.rotation = Quaternion.AngleAxis(_rotateMove, Vector3.up) * _rotation;
        }
    }
}
