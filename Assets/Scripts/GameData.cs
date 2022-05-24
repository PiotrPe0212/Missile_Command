using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{

    //Stored most of the game data, seting scores, and bonus points. 
    public static GameData Instance;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bonusCities;
    [SerializeField] private Text _bonusMissiles;
    public int CurrentBonus { get; private set; }
    public int WavesNumber { get; private set; }
    private int _currentLevel;
    private int _score;
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
        _currentLevel = 0;
        _score = 0;
        ScoreUpdate();
        NewLevelUpdate();
    }

    private void NewLevelUpdate()
    {
        _totalMissilesNumber = 30;
        _citiesNumber = 6;
        _currentLevel++;
    }
    private void RecalculateParameters()
    {
        if (_currentLevel % 5 == 0)
        {
            CurrentBonus = (int)Mathf.Round(_currentLevel * 0.3f);
            WavesNumber++;
        }
    }

    //Added each time enemy missile gets destroyed (MissileController).
    public void AddPoints()
    {
        _score += 10 * CurrentBonus;
        ScoreUpdate();
    }

    public void UpdateMissilesNumber(int number)
    {
        _totalMissilesNumber -= number;
    }

    public void UpdateCitiesNumber()
    {
        _citiesNumber--;
    }

    private void ScoreUpdate()
    {
        _scoreText.text = "SCORE: " + _score.ToString();
    }

    private void LevelWinUpdate(GameManager.GameState state)
    {
        if (state != GameManager.GameState.NextLevel) return;
        BonusAdding();
        ScoreUpdate();
        NewLevelUpdate();
        RecalculateParameters();

    }
    //Bonuses after the end of the level.
    private void BonusAdding()
    {
        int missilesBonus = _totalMissilesNumber * CurrentBonus * 10;
        int citiesBonus = _citiesNumber * CurrentBonus * 50;
        _score = _score + missilesBonus + citiesBonus;
        _bonusCities.text = citiesBonus.ToString();
        _bonusMissiles.text = missilesBonus.ToString();
    }
    private void LevelLost(GameManager.GameState state)
    {
        if(state != GameManager.GameState.Lose) return;
        ResetValues();
    }
}
