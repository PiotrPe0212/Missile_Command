using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    //Class for some methods used in different places of the game.


    //Creating new waitforseconds if it's necessary.

    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds WaitHelper(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait)) return wait;
        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    //Calculating Quaternion from angle.
    public static Quaternion ZRotationChange(float angle)
    {
        Quaternion currentAngle = Quaternion.identity;
        Vector3 currentEulerAngle = new Vector3(0f, 0f, angle / Mathf.Deg2Rad);
        currentAngle.eulerAngles = currentEulerAngle;
        return currentAngle;
    }

    //Checking if the element is out of the game plane. 
    public static void OutofBorderCheck(Transform transform, GameObject gameObject)
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        if (xPos < -8.5f || xPos > 9f) Destroy(gameObject);
        if (yPos < -6.5f || yPos > 6f) Destroy(gameObject);

    }
}
