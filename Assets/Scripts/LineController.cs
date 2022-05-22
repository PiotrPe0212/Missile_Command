using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Transform[] points;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        points = null;
        gameObject.SetActive(false);
    }
    
    public void SetLine(Transform[] points)
    {
        _lineRenderer.positionCount = points.Length;
        this.points = points;
        SettingPos(0);
        SettingPos(1);
        gameObject.SetActive(true);
    }

    
    void Update()
    {
        if (points == null) return;
            SettingPos(1);
        
        
    }
private void SettingPos(int i)
    {
        _lineRenderer.SetPosition(i, points[i].position);
    }

}

