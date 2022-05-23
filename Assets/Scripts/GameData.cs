using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bonusCities;
    [SerializeField] private Text _bonusMissiles;
    public int CurrentLevel { get; private set; }
    public int CurrentBonus { get; private set; }
    public int WavesNumber { get; private set; }

    public int Score { get; private set; }
    private int _totalMissilesNumber;
    private int _citiesNumber;
    private void Awake()
    {
        Instance = this;
        GameManager.OnGameStateChange += LevelWinUpdate;
        GameManager.OnGameStateChange += LevelLost;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= LevelWinUpdate;
        GameManager.OnGameStateChange -= LevelLost;
    }
    private void Start()
    {
        ResetValues();
    }


    private void ResetValues()
    {
        
        CurrentBonus = 1;
        WavesNumber = 5;
        CurrentLevel = 0;
        Score = 0;
        ScoreUpdate();
        NewLevelUpdate();
    }

    private void NewLevelUpdate()
    {
        _totalMissilesNumber = 30;
        _citiesNumber = 6;
        CurrentLevel++;
    }
    private void RecalculateParameters()
    {
        if (CurrentLevel % 5 == 0)
        {
            CurrentBonus = (int)Mathf.Round(CurrentLevel * 0.3f);
            WavesNumber++;
        }
    }

    public void AddPoints()
    {
        Score += 10 * CurrentBonus;
        ScoreUpdate();
    }

    private void ScoreUpdate()
    {
        _scoreText.text = "SCORE: " + Score.ToString();
    }

    public void UpdateMissilesNumber(int number)
    {
        _totalMissilesNumber -= number;
    }

    public void UpdateCitiesNumber()
    {
        _citiesNumber--;
    }

    private void LevelWinUpdate(GameManager.GameState state)
    {
        if (state != GameManager.GameState.NextLevel) return;
        int missilesBonus = _totalMissilesNumber * CurrentBonus*10;
        int citiesBonus = _citiesNumber * CurrentBonus*50;
        Score = Score + missilesBonus + citiesBonus;
        ScoreUpdate();
        _bonusCities.text = citiesBonus.ToString();
        _bonusMissiles.text = missilesBonus.ToString();
        NewLevelUpdate();

    }

    private void LevelLost(GameManager.GameState state)
    {
        if(state != GameManager.GameState.Lose) return;
        ResetValues();
    }
}
