using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class DotProductAsProjectionCylinder : MonoBehaviour
{

    public LineAsCylinder _A_LineCylinder;
    public LineAsCylinder _B_LineCylinder;
    public LineAsCylinder _B_Unit_LineCylinder;
    public LineAsCylinder _A_Projected_LineCylinder;
    public Vector3 a;
    public Vector3 b;
    public Vector3 bUnit;
    public Vector3 aProjected;
    public float[]  diameters = new  float[]  { 0.1f, 0.2f, 0.3f };
    public int updateCount = 0;
    [ExecuteInEditMode]
    void CalculateProjected()
    {
        a = _A_LineCylinder._end; 
        b = _B_LineCylinder._end;
        bUnit = b.normalized;
        float mag = Vector3.Dot(a, bUnit);
        aProjected = bUnit * mag;
    }

    [ExecuteInEditMode]
    void Update()
    {
        CalculateProjected();
        _B_Unit_LineCylinder._end = bUnit;
        _B_Unit_LineCylinder._lineThickness = diameters[2];
        _A_Projected_LineCylinder._end =  aProjected;
        if (_A_Projected_LineCylinder._end.magnitude > _B_LineCylinder._end.magnitude)
        {
            _A_Projected_LineCylinder._lineThickness = diameters[0];
            _B_LineCylinder._lineThickness = diameters[1];
        }
        else
        {
            _A_Projected_LineCylinder._lineThickness = diameters[1];
            _B_LineCylinder._lineThickness = diameters[0];
        }
        // Show how Update() is NOT called every frame in Edit Mode:
        // print("Update Count = " + updateCount++);
    }
}
