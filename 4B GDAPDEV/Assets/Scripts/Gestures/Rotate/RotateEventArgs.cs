using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateEventArgs
{
    Touch[] _trackedFingers;
    EnumRotateDirection _eDir;
    float _rotationDelta;
    float _angle;
    GameObject _hitObject;

    public Touch[] TrackedFingers
    {
        get { return _trackedFingers; }
    }
    public EnumRotateDirection Direction
    {
        get { return _eDir; }
    }

    public float RotationDelta
    {
        get { return _rotationDelta; }
    }
    public float Angle
    {
        get { return this._angle; }
    }
    public GameObject HitObject { 
        get { return _hitObject; }
        set { _hitObject = value; }
    }


    public RotateEventArgs(Touch[] trackedFingers, EnumRotateDirection enumRotateDirection, float rotationDelta, float angle, GameObject hitObj = null)
    {
        this._trackedFingers = trackedFingers;
        this._eDir = enumRotateDirection;
        this._rotationDelta = rotationDelta;
        this._angle = angle;
        this._hitObject = hitObj;
    }
}

public enum EnumRotateDirection
{
    CLOCKWISE,
    COUNTERCLOCKWSE
}