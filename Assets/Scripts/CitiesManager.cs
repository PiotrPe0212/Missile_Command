using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitiesManager : MonoBehaviour
{
    [SerializeField] private GameObject _city;
    [SerializeField] private GameObject _missileBatterie;
    [SerializeField] private GameObject _container;
   
    public int[] CitiesArray;
    
    public int[] BatteriesArray;
    public static CitiesManager Instance;
    private bool _waitControll = false;
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


    }

    private void Update()
    {
        if (GameManager.Instance.State != GameManager.GameState.PlayGame) return;
        if (_waitControll) return;
        StartCoroutine(Routine());

    }

    private void CitiesInit(int length, float offset, float distance, int[] array, GameObject element)
    {
        float additionalDistance = 0;
        for (int i = 1; i <= length; i++)
        {
            print("tut" + i);
            if (array[i-1] == 0)
            {
                if (i >= 4) additionalDistance = 2.5f;
                GameObject createdObject;
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
            GameManager.Instance.GameStateUpdate(GameManager.GameState.Lose);
        }
        _waitControll = false;
    }
}
