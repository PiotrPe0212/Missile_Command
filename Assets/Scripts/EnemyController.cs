using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _missile;
    private bool waitController = false;
    void Start()
    {
        
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
       yield return  Helpers.WaitHelper(Random.Range(1, 3));
        float randomAngle = Random.Range(2*Mathf.PI/3, Mathf.PI)*(Random.Range(0,2)*2-1);
        missile = Instantiate(_missile, transform.position, Helpers.ZRotationChange(randomAngle));
        missile.tag = "EnemyMissile";
        waitController = false;
    }

   
}
