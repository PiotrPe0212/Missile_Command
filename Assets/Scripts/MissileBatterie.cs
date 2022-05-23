using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBatterie : MonoBehaviour
{
    private GameObject _pointer;
    [SerializeField] private GameObject _missile;
    private GameObject _container;

    public int BatterieNumber;
    private int _missilesInitNumber = 10;
    private int _missilesNumber;
    private float _xPos;
    private float _yPos;
    private float _angle;

    private void Awake()
    {
        GameManager.OnGameStateChange += NextLevel;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= NextLevel;
    }
    void Start()
    {

        _pointer = GameObject.Find("Pointer");
        _container = GameObject.Find("FriendMissilesContainer");
        _xPos = transform.position.x;
        _yPos = transform.position.y;
        _missilesNumber = _missilesInitNumber;

    }


    void Update()
    {
        if (GameManager.Instance.State != GameManager.GameState.PlayGame) return;
        if (_missilesNumber == 0) return;
        if (Input.GetKeyDown(KeyCode.Alpha1) && BatterieNumber == 1) FirePressed();
        if (Input.GetKeyDown(KeyCode.Alpha2) && BatterieNumber == 2) FirePressed();
        if (Input.GetKeyDown(KeyCode.Alpha3) && BatterieNumber == 3) FirePressed();
    }

    public void NumerationAdd(int number)
    {
        BatterieNumber = number;
    }

    public void ImDoomed()
    {
        CitiesManager.Instance.BatteriesArray[BatterieNumber - 1] = 0;
        GameData.Instance.UpdateMissilesNumber(_missilesNumber);
    }
    private void FirePressed()
    {
        _pointer.GetComponent<PointerController>().TargetCreating();
        AngleCalculating();
        MissileCreating();
        _missilesNumber--;
        GameData.Instance.UpdateMissilesNumber(1);
        if (_missilesNumber == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
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
        missile.transform.parent = _container.transform;
        missile.tag = "FriendMissile";
    }

    private void NextLevel(GameManager.GameState state)
    {
        if (state != GameManager.GameState.PlayGame) return;
        {
            _missilesNumber = _missilesInitNumber;
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

}
