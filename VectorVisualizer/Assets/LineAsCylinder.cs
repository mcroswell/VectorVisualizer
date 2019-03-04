using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LineAsCylinder : MonoBehaviour
{
    public Vector3 _start;
    public Vector3 _end;
    public float _lineThickness = 0.2f;
    public Vector3 _originalFromRot = new Vector3(0, 1, 0);
    public float _magnitude;
    [ExecuteInEditMode]
    void Start()
    {
     
    }

    public float GetMagnitude()
    {
        Vector3 v = _end - _start;
        if (this.name == "bUnit")
            print("Magnitude = " + Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z));
        return v.magnitude;
    }

    [ExecuteInEditMode]
    void Update()
    {
        _magnitude = GetMagnitude();
        transform.localPosition = _start;
        Vector3 v = _end - _start;
        Quaternion rot = Quaternion.FromToRotation(_originalFromRot, v);
        transform.rotation = rot;
        transform.localScale = new Vector3(_lineThickness, v.magnitude, _lineThickness);
    }
}
