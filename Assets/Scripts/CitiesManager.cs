using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitiesManager : MonoBehaviour
{
    [SerializeField] private GameObject _city;
    [SerializeField] private GameObject _missileBatterie;
    //[HideInInspector]
    public int[] CitiesArray;
    public int[] BatteriesArray;
    public static CitiesManager Instance;
    private bool _gameOver;
    private bool _waitControll = false;
    private bool _play;
    private void Awake()
    {
        Instance = this;
        GameManager.OnGameStateChange += LevelInitialization;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= LevelInitialization;

    }
    void Start()
    {
        CitiesArray = new int[6];
        BatteriesArray = new int[3];
        _play = false;

    }

    private void Update()
    {
        if (_waitControll) return;
        if (!_play) return;
        StartCoroutine(Routine());

    }

    private void CitiesInit(int length, float offset, float distance, int[] array, GameObject element)
    {
        float additionalDistance = 0;
        for (int i = 1; i <= length; i++)
        {
            if (array[i - 1] == i) return;
            if (i >= 4) additionalDistance = 2.5f;
            GameObject createdObject;
            createdObject = Instantiate(element, new Vector3(offset + i * distance + additionalDistance, -4, 0), Quaternion.identity);
            array[i - 1] = i;
            createdObject.BroadcastMessage("NumberationAdd", i);
        }
    }

    private void LevelInitialization(GameManager.GameState state)
    {
        if (state == GameManager.GameState.PlayGame)
        {
            CitiesInit(CitiesArray.Length, -6.5f, 1.5f, CitiesArray, _city);
            CitiesInit(BatteriesArray.Length, -14, 7f, BatteriesArray, _missileBatterie);
            _play = true;
        }
        else _play = false;
    }


    IEnumerator Routine()
    {
        _waitControll = true;
        yield return Helpers.WaitHelper(1);
        CitiesPresenceCheck();

    }
    private void CitiesPresenceCheck()
    {

        int destroyedCities = 0;
        for (int i = 0; i < CitiesArray.Length; i++)
        {
            if (CitiesArray[i] == 0) destroyedCities++;
        }
        if (destroyedCities == 6)
        {
            _gameOver = true;
            GameManager.Instance.GameStateUpdate(GameManager.GameState.Lose);
        }
        _waitControll = false;
    }
}
