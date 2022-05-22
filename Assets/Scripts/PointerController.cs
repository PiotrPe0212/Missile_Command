using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [SerializeField] private GameObject MissileTarget;
    private float _pointerSpeed = 5;
    private float _minXValue = -7.4f;
    private float _minYValue = -2.5f;
    private float _maxXValue = 7.4f;
    private float _maxYValue = 4.3f;
    private Vector2 _initialPointerPos;
    private Vector2 _actualPointerPos;
    void Start()
    {
        _initialPointerPos.x = (_minXValue + _maxXValue) / 2;
        _initialPointerPos.y = (_minYValue + _maxYValue) / 2;
        _actualPointerPos = _initialPointerPos;
        transform.position = _initialPointerPos;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) PointerMove("left");
        if (Input.GetKey(KeyCode.RightArrow)) PointerMove("right");
        if (Input.GetKey(KeyCode.UpArrow)) PointerMove("up");
        if (Input.GetKey(KeyCode.DownArrow)) PointerMove("down");
        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3)) TargetCreating();

    }

    private void PointerMove(string direction)
    {
        Vector3 directionVector = Vector3.zero;


        BorderLimits();

        switch (direction)
        {
            case "left":
                directionVector = -Vector3.right;
                break;

            case "right":
                directionVector = Vector3.right;
                break;

            case "up":
                directionVector = Vector3.up;
                break;

            case "down":
                directionVector = -Vector3.up;
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(direction), direction, "Wrong argument!");
        }

        transform.Translate(directionVector * Time.deltaTime * _pointerSpeed);
        _actualPointerPos = transform.position;

    }

    private void BorderLimits()
    {
        if (_actualPointerPos.x > _maxXValue)
        {
            _actualPointerPos.x = _maxXValue;
            transform.position = _actualPointerPos;
            return;
        }
        if (_actualPointerPos.y > _maxYValue)
        {
            _actualPointerPos.y = _maxYValue;
            transform.position = _actualPointerPos;
            return;
        }
        if (_actualPointerPos.x < _minXValue)
        {
            _actualPointerPos.x = _minXValue;
            transform.position = _actualPointerPos;
            return;
        }
        if (_actualPointerPos.y < _minYValue)
        {
            _actualPointerPos.y = _minYValue;
            transform.position = _actualPointerPos;
            return;
        }
    }

    private void TargetCreating()
    {
        Instantiate(MissileTarget, _actualPointerPos, Quaternion.identity);

    }
}
