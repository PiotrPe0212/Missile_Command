using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private LineController _lineController;
    void Start()
    {
        Transform[] points;
        points = new Transform[2];
        points[0] = transform.parent.gameObject.transform;
        points[1] = transform;
        _lineController.SetLine(points);
        

    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision);
        if (collision.tag != "Target") return;
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
   


}
