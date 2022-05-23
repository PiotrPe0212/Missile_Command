using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _missile;
    private GameObject _container;
    private bool waitController = false;
    void Start()
    {
        _container = GameObject.Find("EnemyMissilesContainer");
    }

    
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * 1);
        Helpers.OutofBorderCheck(transform, gameObject);
        if (!waitController) StartCoroutine( MissleGenerating());
    }

   IEnumerator MissleGenerating()
    {
        GameObject missile;
        waitController = true;
       yield return  Helpers.WaitHelper(Random.Range(2, 4));
        float randomAngle = Random.Range(2*Mathf.PI/3, Mathf.PI)*(Random.Range(0,2)*2-1);
        missile = Instantiate(_missile, transform.position, Helpers.ZRotationChange(randomAngle));
        missile.transform.parent = _container.transform;
        missile.tag = "EnemyMissile";
        waitController = false;
    }

   
}
