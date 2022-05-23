using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameState _gamestate;
    // Start is called before the first frame update

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
                break;
            case GameState.AwaitMove:
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