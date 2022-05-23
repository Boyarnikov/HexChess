using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnut> _units;
    [SerializeField] public BaseUnit _player;

    void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnut>("Units").ToList();
        Debug.Log(_units.Count.ToString() + " units loaded");
    }

    public void SpawnPlayer() {
        //var hero = Instantiate(GetRandomEnemy<BasePlayer>(Type.player));
        //var hero = Instantiate(_player);
        var hero = Instantiate(GetRandomEntity<BasePlayer>(Type.player));
        var tile = GridManager.Instance.GetTile(new Vector2(0, 0));
        if (tile==null) {
            Debug.Log("NULL");
        }
        hero.SetTile(tile);
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
