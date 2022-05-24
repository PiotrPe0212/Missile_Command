using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Handle the current state of the game. Turns up and down panels and handle buttons actions.
    public static GameManager Instance;
    public GameState State;
    [SerializeField] private GameObject _menuPlane;
    [SerializeField] private GameObject _nextLevelPlane;
    [SerializeField] private GameObject _gameOverPlane;
    public static event System.Action<GameState> OnGameStateChange;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        _menuPlane.SetActive(false);
        _nextLevelPlane.SetActive(false);
        _gameOverPlane.SetActive(false);
        GameStateUpdate(State);
    }
    public void GameStateUpdate(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                _menuPlane.SetActive(true);
                break;
            case GameState.PlayGame:
                _menuPlane.SetActive(false);
                _nextLevelPlane.SetActive(false);
                _gameOverPlane.SetActive(false);
                break;
            case GameState.NextLevel:
                _nextLevelPlane.SetActive(true);
                break;
            case GameState.Lose:
                _gameOverPlane.SetActive(true);
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, "Wrong argument!");
        }
        OnGameStateChange?.Invoke(newState);
    }


    public enum GameState
    {
        MainMenu,
        PlayGame,
        NextLevel,
        Lose
    }

    //Buttons
    public void PlayButton()
    {
        GameStateUpdate(GameState.PlayGame);
    }

    public void MainMenuButton()
    {
        GameStateUpdate(GameState.MainMenu);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
