using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static GameState _gamestate;
    
    public GameState GetState() {
        return _gamestate;
    }

    void Awake() {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState state)
    {
        _gamestate = state;
        switch (state)
        {
            case GameState.GenerateGrid:
                Debug.Log("Generating Grid");
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnPlayer:
                Debug.Log("Spawning Player");
                UnitManager.Instance.SpawnPlayer();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemy(2);
                break;
            case GameState.AwaitMove:
                PlayerControlManager.Instance.StartTurn();
                break;
            case GameState.PlayEvents:
                break;   
            case GameState.MoveEnemies:
                break;
            default:
                Debug.Log("gamestate error");
                break;
        }
        
    }
}

public enum GameState {
    GenerateGrid,

    SpawnPlayer,

    SpawnEnemies,
    AwaitMove,
    PlayEvents,
    MoveEnemies,
}