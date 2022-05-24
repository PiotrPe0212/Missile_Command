using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Explosion pulsation, gets destroyed after x sec.
    private float _time = 3f;
    void FixedUpdate()
    {
        transform.localScale = new Vector3(Mathf.Cos(Time.time), Mathf.Cos(Time.time), 1);
        Destroy(gameObject, _time);

    }
}
