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
        for (var i = 0; i < 10; i++) {
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

    private T GetRandomEntity<T>(Type type) where T : BaseUnit {
        var enemys = from u in _units
                    where u.type.Equals(type) 
                    orderby Random.value
                    select u;
        Debug.Log("found " + enemys.Count().ToString());
        T enemy = (T) enemys.First().unitPrefab;
        return enemy;
    }
}
