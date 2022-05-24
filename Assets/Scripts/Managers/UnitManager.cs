using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnut> _units;
    private List<Tile> readyToSpawnEnemys = new List<Tile>();

    void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnut>("Units").ToList();
        Debug.Log(_units.Count.ToString() + " units loaded");
    }

    public void SpawnPlayer() {
        int spawnRange = 2;
        int amount = 5;
        for (var i = 0; i < amount; i++) {
            var hero = Instantiate(GetRandomEntity<BasePlayer>(Type.player));
            var pos = new Vector2(i, i);
            var tile = GridManager.Instance.GetTile(pos);
            while (tile==null || !tile.Free) {
                pos = RandomEdgeCoordinate(spawnRange);
                tile = GridManager.Instance.GetTile(pos);
            }
            hero.SnapToTile(tile);
        }

        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemy() {
        foreach (var tile in readyToSpawnEnemys) 
        {
            tile.Activate();
            var hero = Instantiate(GetRandomEntity<BaseEnemy>(Type.enemy));
            hero.SnapToTile(tile);
        }
    }

    public void PrepareTilestForEnemys(int amount) {
        int spawnRange = 5;
        Debug.Log("enemy spawning");
        readyToSpawnEnemys = new List<Tile>();
        for (var i = 0; i < amount; i++) {
            var pos = RandomEdgeCoordinate(spawnRange);
            var tile = GridManager.Instance.GetTile(pos);
            while (tile==null || !tile.Free) {
                pos = RandomEdgeCoordinate(spawnRange);
                tile = GridManager.Instance.GetTile(pos);
            }
            readyToSpawnEnemys.Add(tile);
            tile.Deactivate();
        }

        GameManager.Instance.ChangeState(GameState.AwaitMove);
    }

    public Vector2 RandomEdgeCoordinate(int edge) {
        var vec = new Vector2(Random.Range(-edge, edge+1), Random.Range(-edge, edge+1));
        switch (Random.Range(0, 6))
        {
            case 0:
                vec.x = -edge;
                break;
            case 1:
                vec.x = edge;
                break;
            case 2:
                vec.y = -edge;
                break;
            case 3:
                vec.y = edge;
                break;
            case 4:
                vec.x = Random.Range(1, edge);
                vec.y = vec.x - edge;
                break;
            case 5:
                vec.y = Random.Range(1, edge);
                vec.x = vec.y - edge;
                break;
            default:
                break;
        }
        return vec;
    }

    private T GetRandomEntity<T>(Type type) where T : BaseUnit {
        var enemys = from u in _units
                    where u.type.Equals(type) 
                    orderby Random.value
                    select u;
        T enemy = (T) enemys.First().unitPrefab;
        return enemy;
    }
}
