using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Control movement and shooting missiles of each enemy.

    [SerializeField] private GameObject _missile;
    private GameObject _container;
    private bool _waitControl;
    private void Start()
    {
        _container = GameObject.Find("EnemyMissilesContainer");
        _waitControl = false;
    }

    private void FixedUpdate()
    {
        MoveFunction();
        if (_waitControl) return;
        StartCoroutine(MissileGenerating());
    }

    IEnumerator MissileGenerating()
    {

        _waitControl = true;
        yield return Helpers.WaitHelper(Random.Range(2, 4));
        MissileGenFunction();
        _waitControl = false;
    }

    private void MoveFunction()
    {
        transform.Translate(Vector3.right * Time.deltaTime * 1);
        Helpers.OutofBorderCheck(transform, gameObject);
    }
    private void MissileGenFunction()
    {
        GameObject missile;
        float randomAngle = Random.Range(2 * Mathf.PI / 3, Mathf.PI) * (Random.Range(0, 2) * 2 - 1);
        missile = Instantiate(_missile, transform.position, Helpers.ZRotationChange(randomAngle));
        missile.transform.parent = _container.transform;
        missile.tag = "EnemyMissile";

    }

}
