using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitiesManager : MonoBehaviour
{
//Handle creating and recreating cities and missile batteries.

    [SerializeField] private GameObject _city;
    [SerializeField] private GameObject _missileBatterie;
    [SerializeField] private GameObject _container;
    [HideInInspector]
    public int[] CitiesArray;
    [HideInInspector]
    public int[] BatteriesArray;
    public static CitiesManager Instance;
    private bool _waitControl;
    private void Awake()
    {
        Instance = this;
        GameManager.OnGameStateChange += LevelInitialization;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= LevelInitialization;
    }
    private void Start()
    {
        CitiesArray = new int[6];
        BatteriesArray = new int[3];
        _waitControl = false;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.State != GameManager.GameState.PlayGame) return;
        if (_waitControl) return;
        StartCoroutine(CitiesCheck());

    }

    private void CitiesInit(int length, float offset, float distance, int[] array, GameObject element)
    {
        float additionalDistance = 0;
        for (int i = 1; i <= length; i++)
        {
            if (array[i - 1] == 0)
            {
                GameObject createdObject;
                if (i >= 4) additionalDistance = 2.5f;
                createdObject = Instantiate(element, new Vector3(offset + i * distance + additionalDistance, -3.8f, 0), Quaternion.identity);
                createdObject.transform.parent = _container.transform;
                array[i - 1] = i;
                createdObject.BroadcastMessage("NumerationAdd", i);
            }
        }
    }

    private void LevelInitialization(GameManager.GameState state)
    {
        if (state == GameManager.GameState.PlayGame)
        {
            CitiesInit(CitiesArray.Length, -6.5f, 1.5f, CitiesArray, _city);
            CitiesInit(BatteriesArray.Length, -14, 7f, BatteriesArray, _missileBatterie);
        }

    }

    //Each second check how many cities get destroyed. When all of them - game is over.
    IEnumerator CitiesCheck()
    {
        _waitControl = true;
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
            GameManager.Instance.GameStateUpdate(GameManager.GameState.Lose);
        }
        _waitControl = false;
    }
}
