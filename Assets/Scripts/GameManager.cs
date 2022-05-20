using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    public static event System.Action<GameState> OnGameStateChange;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        GameStateUpdate(State);
    }
    public void GameStateUpdate(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:

                break;
            case GameState.PlayGame:

                break;
            case GameState.NextLevel:

                break;
            case GameState.Lose:

                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, "Wrong argument!");
        }
        print(newState);
        OnGameStateChange?.Invoke(newState);
    }


    public enum GameState
    {
        MainMenu,
        PlayGame,
        NextLevel,
        Lose
    }
}
