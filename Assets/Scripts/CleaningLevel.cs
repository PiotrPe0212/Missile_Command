using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningLevel : MonoBehaviour
{
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _enemyMissileContainer;
    [SerializeField] private GameObject _friendMissileContainer;
    [SerializeField] private GameObject _targetsContainer;


    private void Awake()
    {
        GameManager.OnGameStateChange += CleaningLevelCall;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= CleaningLevelCall;
    }

    private void CleaningLevelCall(GameManager.GameState state)
    {
        if (state != GameManager.GameState.PlayGame)
        {
            CleaningFunction(_enemyContainer);
            CleaningFunction(_enemyMissileContainer);
            CleaningFunction(_friendMissileContainer);
            CleaningFunction(_targetsContainer);

        }


    }

    private void CleaningFunction(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
