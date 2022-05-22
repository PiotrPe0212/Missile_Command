using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private LineController _lineController;
    private Transform _initPos;
    private float _missileSpeed = 3;
    void Start()
    {
        _initPos = transform;
        Transform[] points;
        points = new Transform[2];
        points[0] = _initPos;
        points[1] = transform;
        _lineController.SetLine(points);


    }

    void FixedUpdate()
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
        Instantiate(_explosion, transform.position, Quaternion.identity);
        
        if(collision.tag == "Building")collision.gameObject.BroadcastMessage("ImDoomed");
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

}
