using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProductAsProjection : MonoBehaviour
{

    public LineRenderer _A_LineRenderer;
    public LineRenderer _B_LineRenderer;
    public LineRenderer _B_Unit_LineRenderer;
    public LineRenderer _A_Projected_LineRenderer;
    public Vector3 a;
    public Vector3 b;
    public Vector3 bUnit;
    public Vector3 aProjected;

    void CalculateAProjected()
    {
        a = _A_LineRenderer.GetPosition(1);
        b = _B_LineRenderer.GetPosition(1);
        bUnit = b.normalized;
        float mag = Vector3.Dot(a, bUnit);
        aProjected = bUnit * mag;
    }

    [ExecuteInEditMode]
    void Update()
    {
        CalculateAProjected();
        _B_Unit_LineRenderer.SetPosition(1, bUnit);
        _A_Projected_LineRenderer.SetPosition(1, aProjected);
    }
}
