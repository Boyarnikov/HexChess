using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile _tile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTile(Tile tile) {
        if (tile == null || tile._unit != null) {
            return;
        }     
        if (_tile != null)
            _tile._unit = null;
        _tile = tile;
        _tile._unit = this;
    }
}
