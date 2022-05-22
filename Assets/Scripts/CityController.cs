using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    public int CityNumber;
    public void NumberationAdd(int number)
    {
        CityNumber = number;
    }

    public void ImDoomed()
    {
        CitiesManager.Instance.CitiesArray[CityNumber-1] = 0;
    }
}
