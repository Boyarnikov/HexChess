using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;

public class BoardLogic : MonoBehaviour
{
    public GameObject cell;
    private Vector3 x_v = new Vector3(1f, 0, 0);
    private Vector3 y_v = new Vector3(-0.2f, 0, 0.866f);
    public int board_size = 5;
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < board_size * 2; i++)
            for (var j = 0; j < board_size * 2; j++)
                if (abs(i-j) <= board_size)
                    Instantiate(cell, x_v * i + y_v * j, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
