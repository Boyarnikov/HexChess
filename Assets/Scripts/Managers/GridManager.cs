using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] public Tile cell;
    [SerializeField] private Vector3 x_v = new Vector3(1f, 0, 0);
    [SerializeField] private Vector3 y_v = new Vector3(-0.5f, 0, 0.866f);
    [SerializeField] private int board_size = 5;

    private Dictionary<Vector2, Tile> _grid;

    void Awake() {
        Instance = this;
    }

    public void GenerateGrid() {
        _grid = new Dictionary<Vector2, Tile>();
        var offset_x = -board_size * x_v / 2;
        for (var i = 0; i < board_size * 2 + 1; i++)
            for (var j = 0; j < board_size * 2 + 1; j++)
                if (abs(i-j) <= board_size) {
                    var tile = Instantiate(cell, offset_x + x_v * i + y_v * j, Quaternion.identity);
                    tile.name = $"Tile {i} {j}";
                    tile.InitColor(i, j);
                    _grid[new Vector2(i, j)] = tile;
                }    

        GameManager.Instance.ChangeState(GameState.SpawnPlayer);
    }

    public Tile GetTile(Vector2 pos) {
        if (_grid.TryGetValue(pos, out var tile)) {
            return tile;
        }
        return null;
    }

    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        
    }
}
