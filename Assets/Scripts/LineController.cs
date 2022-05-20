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
    }
    
    public void SetLine(Transform[] points)
    {
        _lineRenderer.positionCount = points.Length;
        this.points = points;
    }

    
    void Update()
    {
        if (points == null) return;
        for( int i = 0; i<points.Length; i++)
        {
            _lineRenderer.SetPosition(i, points[i].position);
        }
        
    }
}
