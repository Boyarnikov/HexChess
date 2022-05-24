using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnut> _units;

    void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnut>("Units").ToList();
        Debug.Log(_units.Count.ToString() + " units loaded");
    }

    public void SpawnPlayer() {
        for (var i = 0; i < 2; i++) {
            var hero = Instantiate(GetRandomEntity<BasePlayer>(Type.player));
            var pos = new Vector2(Random.Range(-5, 6), Random.Range(-5, 6));
            var tile = GridManager.Instance.GetTile(pos);
            while (tile==null || !tile.Free) {
                pos = new Vector2(Random.Range(-5, 6), Random.Range(-5, 6));
                tile = GridManager.Instance.GetTile(pos);
            }
            hero.SetTile(tile);
        }

        GameManager.Instance.ChangeState(GameState.AwaitMove);
    }

    public void SpawnEnemy(int amount) {
        Debug.Log("enemy spawning");
        for (var i = 0; i < amount; i++) {
            var hero = Instantiate(GetRandomEntity<BaseEnemy>(Type.enemy));
            var pos = RandomEdgeCoordinate(5);
            var tile = GridManager.Instance.GetTile(pos);
            while (tile==null || !tile.Free) {
                pos = RandomEdgeCoordinate(5);
                tile = GridManager.Instance.GetTile(pos);
            }
            hero.SetTile(tile);
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
                vec.y = vec.x - 5;
                break;
            case 5:
                vec.y = Random.Range(1, edge);
                vec.x = vec.y - 5;
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
