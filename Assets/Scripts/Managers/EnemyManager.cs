using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private bool _isMoving = false;
    private float _movingTick = 0;
    private float _movingDelta = 0.02f;
    private int _movingSpawned = 0;
    private BaseEnemy[] enemys;

    void Awake()
    {
        Instance = this;
    }

    public void MoveEnemies() {
        enemys = (BaseEnemy[])FindObjectsOfType(typeof(BaseEnemy));
        _isMoving = true;
        _movingTick = -4;
        _movingSpawned = -1;
    }

    private void Update() {
        if (_isMoving) {
            int _item = (int)_movingTick;
            if (_movingSpawned < _item && _item >= 0 && _item < enemys.Length) {
                enemys[_item].Move();
                _movingSpawned++;
            }
            if (_movingTick > enemys.Length || enemys.Length == 0) {
                GameManager.Instance.ChangeState(GameState.SpawnEnemies);
                _isMoving = false;
            }
            _movingTick += _movingDelta;
        }
    }
}

