using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningLevel : MonoBehaviour
{

    //Cleans a board after each level. 

    [SerializeField] private GameObject[] _containers;

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
            for (int i = 0; i < _containers.Length; i++)
            {
                CleaningFunction(_containers[i]);
            }

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
