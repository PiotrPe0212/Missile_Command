using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBatterie : MonoBehaviour
{
    // Handle friendly missiles shoot.

    public int BatterieNumber;
    [SerializeField] private GameObject _missile;
    private SpriteRenderer _spriteRenderer;
    private PointerController _pointerController;
    private GameObject _pointer;
    private GameObject _container;
    private int _missilesInitNumber;
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
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _pointer = GameObject.Find("Pointer");
        _pointerController = _pointer.GetComponent<PointerController>();
        _container = GameObject.Find("FriendMissilesContainer");
        _xPos = transform.position.x;
        _yPos = transform.position.y;
        _missilesInitNumber = 10;
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

    private void FirePressed()
    {
        _pointerController.TargetCreating();
        AngleCalculating();
        MissileCreating();
        _missilesNumber--;
        GameData.Instance.UpdateMissilesNumber(1);
        if (_missilesNumber == 0)
        {
            _spriteRenderer.color = Color.red;
        }
    }
    //Calculating missile angle.
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
            _spriteRenderer.color = Color.yellow;
        }
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
}
