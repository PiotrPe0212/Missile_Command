using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    //Each city has a different number, when destroyed sends information to cities controller and data manager.
    private int _cityNumber;
    public void NumerationAdd(int number)
    {
        _cityNumber = number;
    }

    public void ImDoomed()
    {
        CitiesManager.Instance.CitiesArray[_cityNumber-1] = 0;
        GameData.Instance.UpdateCitiesNumber();
    }
}
