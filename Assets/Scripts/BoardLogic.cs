using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;

public class BoardLogic : MonoBehaviour
{
    [SerializeField] public Tile cell;
    [SerializeField] private Vector3 x_v = new Vector3(1f, 0, 0);
    [SerializeField] private Vector3 y_v = new Vector3(-0.5f, 0, 0.866f);
    [SerializeField] public int board_size = 5;

    void GenerateGrid() {
        for (var i = 0; i < board_size * 2 + 1; i++)
            for (var j = 0; j < board_size * 2 + 1; j++)
                if (abs(i-j) <= board_size) {
                    var tile = Instantiate(cell, x_v * i + y_v * j, Quaternion.identity);
                    tile.name = $"Tile {i} {j}";
                    tile.InitColor(i, j);
                }    
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
