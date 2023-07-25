using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    GameRunning,
    GamePaused,
    Victory,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.GameRunning);
    }

    public void ChangeState(GameState _state)
    {
        this.gameState = _state;

        switch (this.gameState)
        {
            case GameState.GameRunning:
                break;
            case GameState.GamePaused:
                break;
            case GameState.Victory:
                break;
            case GameState.GameOver:
                break;
        }
    }
}
