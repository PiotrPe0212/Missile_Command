using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBatterie : MonoBehaviour
{
    private GameObject _pointer;
    [SerializeField] private GameObject _missile;

    public int BatterieNumber;

    private int _missilesNumber;
    private float _xPos;
    private float _yPos;
    private float _angle;
    void Start()
    {

        _pointer = GameObject.Find("Pointer");
        _xPos = transform.position.x;
        _yPos = transform.position.y;


    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && BatterieNumber == 1) FirePressed();
        if (Input.GetKeyDown(KeyCode.Alpha2) && BatterieNumber == 2) FirePressed();
        if (Input.GetKeyDown(KeyCode.Alpha3) && BatterieNumber == 3) FirePressed();
    }

    public void NumberationAdd(int number)
    {
        BatterieNumber = number;
    }

    public void ImDoomed()
    {
        CitiesManager.Instance.BatteriesArray[BatterieNumber-1] = 0;
    }
    private void FirePressed()
    {
        AngleCalculating();
        MissileCreating();

    }
    private void AngleCalculating()
    {
        float pointerXPos = _pointer.transform.position.x;
        float pointerYPos = _pointer.transform.position.y;

        _angle = Mathf.Atan(Mathf.Abs(_xPos - pointerXPos) / Mathf.Abs(_yPos - pointerYPos));
        if (pointerXPos > _xPos) _angle = -_angle;
    }
    private void MissileCreating()
    {
        GameObject missile;
        missile = Instantiate(_missile, transform.position, Helpers.ZRotationChange(_angle));
        missile.tag = "FriendMissile";
    }

}
