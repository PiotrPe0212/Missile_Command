using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
   
    void FixedUpdate()
    {
        transform.localScale = new Vector3(Mathf.Cos(Time.time), Mathf.Cos(Time.time), 1);
        Destroy(gameObject, 3f);
        
    }
}
