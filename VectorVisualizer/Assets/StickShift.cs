using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StickShift : MonoBehaviour
{
    public LineAsCylinder _stick;
    public SphereCollider _sphereCollider; // Only one needed fof all sticks. Sibling to stick.
    public float _offset;
    [ContextMenuItem("Reset Radius", "SetRadius")]
    public float _radius;
    public bool _mDown = false;
    public Vector3 _mouseDownRotVec;
    public bool _mDrag = false;
    public LayerMask _sphereLayerMask;

 
    void Start()
    {
        SetRadius();
        _sphereCollider.enabled = false;
    }

    void SetRadius()
    {
        Vector3 stickVec = _stick._end - _stick._start;
        _radius = stickVec.magnitude; // My intention was to set position of control at end of stick before moving stick.
        // That may still be (TODO) but now we'll use it for control sphere:
        _sphereCollider.radius = _radius+_offset;

    }
    [ContextMenu ("Position At End Of Stick")]
    void MoveToEndOfStick()
    {
        Vector3 stickVec = _stick._end - _stick._start;
        Vector3 pos = stickVec + _stick.transform.position;
        pos = pos * (1f + _offset);
        transform.position = pos;
    }

    private void OnMouseDown()
    {
        Debug.Log("Down");
        _mDown = true;
        _mouseDownRotVec = _stick.transform.up;
        _sphereCollider.enabled = true;
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider == _sphereCollider)
            {
                Vector3 pointOnSphere = hit.point - _sphereCollider.center;
                print("Hit " + hit.transform.name + " at relative (local) position on sphere " + pointOnSphere);
                // Now, figure out rotation. This will be done at mouseDrag and involve
                // going from downRot to newRot over and over.
                _mouseDownRotVec = _stick.transform.up;
            }
        }*/

    }
    private void OnMouseDrag()
    {
        //Debug.Log("moving");
        //Vector3 mouse3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.Log(name + ".position=" + transform.position + "mouse3D=" + mouse3D + " ray=" + ray);
        //// Debug.Log("Delta=" + e.delta + " Pos=" + e.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red, .5f);
        _mDown = false;
        _mDrag = true;
        //print("Dragging");
        // Same deal as above in mouseDown:
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, _sphereLayerMask))
        {
            if (hit.collider == _sphereCollider)
            {
                Vector3 pointOnSphere = hit.point - _sphereCollider.center;
            //print("Hit " + hit.transform.name + " at relative (local) position on sphere " + pointOnSphere);
                // Now, figure out rotation. This will be done at mouseDrag and involve
                // going from downRot to newRot over and over.
                // Don't need since LineAsCylinder (our stick) will do this:
                //Quaternion rot = Quaternion.FromToRotation(_mouseDownRotVec, pointOnSphere);
                _stick._end = _stick._start + ( _stick.GetMagnitude() * pointOnSphere.normalized );
            }
            // Frank was here.
        }
    }

    private void OnMouseUp()
    {
        //Debug.Log("Down");
        _mDown = false;
        _mDrag = false;
        _sphereCollider.enabled = false;
    }

    void OnGUI()
    {
        //if (_mDown)
        {
            Event e = Event.current;
            if (e.isMouse)
            {
                //Vector3 mouse3D = Camera.main.ScreenToWorldPoint(e.mousePosition);
                //Ray ray = Camera.main.ScreenPointToRay(e.mousePosition);

                //Debug.Log("transform.position=" + transform.position + "mouse3D=" + mouse3D + " ray=" + ray);
                //// Debug.Log("Delta=" + e.delta + " Pos=" + e.mousePosition);
                //Debug.DrawRay(ray.origin, ray.direction, Color.red, .5f);
                 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (_radius <= 0.0000001f)
        //    SetRadius();
        MoveToEndOfStick();

 
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 100))
        //{
        //    print("Hit " + hit.transform.name);
        //}
    
    }


}
