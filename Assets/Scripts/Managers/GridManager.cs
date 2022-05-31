using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] public Tile cell;
    [SerializeField] private Vector3 x_v = new Vector3(1f, 0, 0);
    [SerializeField] private Vector3 y_v = new Vector3(-0.5f, 0, 0.866f);
    [SerializeField] public int board_size = 5;
    
    public static Vector3 mousePos = new Vector3(0, 0, 0);
    private Dictionary<Vector2, Tile> _grid;
    private Plane _plane;
     

    void Awake() {
        Instance = this;
    }

    public void GenerateGrid() {
        _grid = new Dictionary<Vector2, Tile>();
        var offset_x = GetMid();
        for (var i = 0; i < board_size * 2 + 1; i++)
            for (var j = 0; j < board_size * 2 + 1; j++)
                if (abs(i-j) <= board_size) {
                    var tile = Instantiate(cell,
                        offset_x + x_v * (i - board_size) + 
                        y_v * (j - board_size), Quaternion.identity,
                        transform);
                    tile.name = $"Tile {i - board_size} {j - board_size}";
                    tile.Init(i - board_size, j - board_size);
                    _grid[new Vector2(i - board_size, j - board_size)] = tile;
                }    

        GameManager.Instance.ChangeState(GameState.SpawnPlayer);
    }

    public List<Tile> GetAllTiles() {
        return _grid.Values.ToList();;
    }

    public Tile GetTile(Vector2 pos) {
        if (_grid.TryGetValue(pos, out var tile)) {
            return tile;
        }
        return null;
    }

    public Vector3 GetMid() {
        return -board_size * x_v / 2 + (x_v + y_v) * board_size;
    }

    void UpdateMousePosition() 
    {
        float distance;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (_plane.Raycast(ray, out distance)) {
            mousePos = ray.GetPoint(distance);
        }
    }

    void Start()
    {
        _plane = new Plane(Vector3.up, transform.position);
    }

    void Update()
    {
        UpdateMousePosition();
    }
}
