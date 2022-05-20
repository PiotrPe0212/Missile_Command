using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = new Vector3(Mathf.Cos(Time.time), Mathf.Cos(Time.time), 1);
        Destroy(gameObject, 3f);
        
    }
}
