using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Handle spawning enemies for each level.

    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _enemyMissileContainer;
    private int _wavesNumber;
    private int _wavesCounter;
    private bool _waveEnd = false;
    private bool _waitController;
    private float _initGeneratingPos;

    private void Awake()
    {
        GameManager.OnGameStateChange += LevelInitialization;
        _initGeneratingPos = 6;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= LevelInitialization;

    }

    void Update()
    {
        if (GameManager.Instance.State != GameManager.GameState.PlayGame) return;
        if (_waveEnd)
        {
            IsLevelCompliete();
        }
        if (_waitController || _waveEnd) return;
        StartCoroutine(EnemyGenerating());

    }

    IEnumerator EnemyGenerating()
    {
        _waitController = true;
        yield return Helpers.WaitHelper(4);
        GeneratingFunction();
    }

    private void GeneratingFunction()
    {
        GameObject enemy;
        float yPos = _initGeneratingPos;
        if (Random.Range(0, 5) == 5) yPos = Random.Range(2.0f, 4.0f);
        Vector3 randomPosition = new Vector3(-8.5f, yPos, 0);
        enemy = Instantiate(_enemy, randomPosition, Quaternion.identity);
        enemy.transform.parent = _enemyContainer.transform;
        _waitController = false;
        _wavesCounter++;
        if (_wavesCounter == _wavesNumber) _waveEnd = true;
    }

    private void LevelInitialization(GameManager.GameState state)
    {
        if (state == GameManager.GameState.PlayGame)
        {
            _wavesNumber = GameData.Instance.WavesNumber;
            ResetParams();
            GeneratingFunction();
        }
    }

    private void ResetParams()
    {
        _wavesCounter = 0;
        _waveEnd = false;
        _waitController = false;
    }

    //Changing state on level completed when all enemy missiles are destroyed.
    private void IsLevelCompliete()
    {
        if (_enemyMissileContainer.transform.childCount == 0)
        {
            GameManager.Instance.GameStateUpdate(GameManager.GameState.NextLevel);
        }
    }
}
