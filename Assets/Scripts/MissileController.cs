using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{

    //Handle move and colisions of the missile.

    [SerializeField] private GameObject _explosion;
    [SerializeField] private LineController _lineController;
    private float _missileSpeed;
    void Start()
    {
        Transform[] points;
        points = new Transform[2];
        points[0] = transform;
        points[1] = transform;
        _lineController.SetLine(points);
        if (gameObject.tag == "EnemyMissile")
        {
            _lineController.SetColor(Color.red);
            _missileSpeed = GameData.Instance.CurrentBonus * 0.7f;

        }
        if (gameObject.tag == "FriendMissile")
        {
            _lineController.SetColor(Color.yellow);
            _missileSpeed = 5;
        }


    }

    void FixedUpdate()
    {
        MoveFunction();
    }

    private void MoveFunction()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _missileSpeed);
        Helpers.OutofBorderCheck(transform, gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (gameObject.tag == "FriendMissile")
        {
            if (tag == "Target" ||
                tag == "Enemy" ||
                tag == "EnemyMissile") Destruction(collision);
        }
        else if (gameObject.tag == "EnemyMissile")
        {

            if (tag == "Explosion" ||
                tag == "Building" ||
                tag == "FriendMissile") Destruction(collision);
        }

    }

    private void Destruction(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "Explosion" || tag == "FriendMissile") GameData.Instance.AddPoints();
        if (tag == "Building") collision.gameObject.BroadcastMessage("ImDoomed");
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

}
