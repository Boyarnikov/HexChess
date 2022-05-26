using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private bool _isMoving = false;
    private float _timer = 0;
    private float _timerDelta = 0.02f;
    private float _timerTime = 1f;
    private float _timerSetup = 1f;
    private int _enemyInd = 0;
    private List<BaseEnemy> enemys;

    void Awake()
    {
        Instance = this;
    }

    public void MoveEnemies() {
        _isMoving = true;
        _timer = -_timerSetup;
        _enemyInd = -2;
    }

    public void getEnemies() {
        enemys = new List<BaseEnemy>();
        BaseEnemy[] e = (BaseEnemy[])FindObjectsOfType(typeof(BaseEnemy));
        foreach (var enemy in e)
        {
            enemys.Add(enemy);
        }
        Debug.Log("get enemys " + enemys.Count);
    }

    private void Update() {
        if (_isMoving) {
            _timer += _timerDelta;
            if (_timer > _timerTime) {
                _timer = 0;
                Debug.Log("id " + _enemyInd);
                if (_enemyInd >= 0 && _enemyInd < enemys.Count) {
                    Debug.Log("moving enemy " + _enemyInd);
                    var enemy = enemys[_enemyInd];
                    Debug.Log(enemy);
                    enemy.Move();
                }
                _enemyInd++;
                if (_enemyInd == -1) getEnemies();

                if (_enemyInd >= enemys.Count) {
                    _isMoving = false;
                    GameManager.Instance.ChangeState(GameState.SpawnEnemies);
                }
            }
        }
    }
}

