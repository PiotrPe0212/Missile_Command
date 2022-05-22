using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyContainer;
    public int wavesNumber = 8;
    private int _wavesCounter;
    private bool _waveEnd = false;
    private bool _waitController;
    private bool _play = false;
    private void Awake()
    {
        GameManager.OnGameStateChange += LevelInitialization;
        GameManager.OnGameStateChange += DestrtoyAfterEnd;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= LevelInitialization;
        GameManager.OnGameStateChange -= DestrtoyAfterEnd;

    }


    void Update()
    {
        if (!_play) return;
        if (_waveEnd) {
            IsLevelCompliete();
        }// ????????????????????????????????????????????????????????????
        if (_waveEnd) return;
        if (_waitController) return;
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
        float yPos = 6;
        if (Random.Range(0, 5) == 5) yPos = Random.Range(2.0f, 6.0f);
        Vector3 randomPosition = new Vector3(-8.5f, yPos, 0);
        enemy = Instantiate(_enemy, randomPosition, Quaternion.identity);
        enemy.transform.parent = _enemyContainer.transform;
        _waitController = false;
        _wavesCounter++;
        if (_wavesCounter == wavesNumber) _waveEnd = true;
    }

    private void LevelInitialization(GameManager.GameState state)
    {
        if (state == GameManager.GameState.PlayGame)
        {
            _play = true;
            ResetParams();
        }
        else _play = false;
    }

    private void DestrtoyAfterEnd(GameManager.GameState state)
    {
        if (state != GameManager.GameState.PlayGame)
        {
           
                foreach (Transform child in _enemyContainer.transform)
                {
                    Destroy(child.gameObject);
                }
            
        }
    }

    private void ResetParams()
    {
        _wavesCounter = 0;
        _waveEnd = false;
        _waitController = false;
    }

    private void IsLevelCompliete()
    {
        if (_enemyContainer.transform.childCount == 0)
        {
            GameManager.Instance.GameStateUpdate(GameManager.GameState.NextLevel);
        }
        
    }
}
