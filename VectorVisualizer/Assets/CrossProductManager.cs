using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CrossProductManager : MonoBehaviour
{

    public LineAsCylinder _A_LineCylinder;
    public LineAsCylinder _B_LineCylinder;
    public LineAsCylinder _CrossProduct;
    public bool _reverseOrder = false;
    private Vector3 a;
    private Vector3 b;
    private Vector3 c;

    [ExecuteInEditMode]
    void CalculateCrossProd()
    {
        a = _A_LineCylinder._end;
        b = _B_LineCylinder._end;
        if (_reverseOrder)
            c = Vector3.Cross(b, a);
        else
            c = Vector3.Cross(a, b);

    }

    [ExecuteInEditMode]
    void Update()
    {
        CalculateCrossProd();
        _CrossProduct._end = c;

    }
}
