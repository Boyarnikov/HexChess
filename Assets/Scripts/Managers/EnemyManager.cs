using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void MoveEnemies() {
        BaseEnemy[] enemys = (BaseEnemy[])FindObjectsOfType(typeof(BaseEnemy));
        foreach (var enemy in enemys)
        {
            enemy.Move();
        }
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }
}

