using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadEventArgs 
{
    private Touch[] _trackedFingers;
    private float _distanceDelta;
    private GameObject _hitObject;

    public SpreadEventArgs(Touch[] trackedFingers, float distanceDelta, GameObject hitObject = null)
    {
        _trackedFingers = trackedFingers;
        _distanceDelta = distanceDelta;
        _hitObject = hitObject;
    }

    public Touch[] TrackedFingers { get { return _trackedFingers; } }
    public float DistanceDelta { get { return _distanceDelta;} }
    public GameObject HitObject { get { return _hitObject; } set { _hitObject = value;  } }
}