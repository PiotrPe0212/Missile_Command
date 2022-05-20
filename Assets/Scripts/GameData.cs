using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int currentLevel { get; private set; }
    public int currentBonus { get; private set; }
    public int wavesNumber { get; private set; }
    public float enemyMissileSpeed { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }


    void Update()
    {

    }
}
