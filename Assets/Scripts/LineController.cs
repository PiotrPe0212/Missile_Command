using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    //Renders line after each missile.

    private LineRenderer _lineRenderer;
    private Transform[] points;
    private Color _color;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        points = null;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (points == null) return;
        SettingPos(1);


    }

    //Setting line from points from the missile.
    public void SetLine(Transform[] points)
    {
        _lineRenderer.positionCount = points.Length;
        this.points = points;
        SettingPos(0);
        SettingPos(1);
        gameObject.SetActive(true);
    }

    //Setting color of the line depends on missile type.
    public void SetColor(Color color)
    {
        this._color = color;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(_color, 0.0f), new GradientColorKey(_color, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1, 0.0f), new GradientAlphaKey(1, 1.0f) }
        );
        _lineRenderer.colorGradient = gradient;

    }

    private void SettingPos(int i)
    {
        _lineRenderer.SetPosition(i, points[i].position);
    }
}

