using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEventArgs : EventArgs {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private Touch[] _trackedFingers;
    public Touch[] TrackedFinger {
        get { return this._trackedFingers; }
    }

    private ERotation _rotation;
    public ERotation Rotation {
        get { return this._rotation; }
    }

    private float _angle;
    public float Angle { 
        get { return this._angle; }
    }

    private GameObject _hitObject;
    public GameObject HitObject {
        get { return _hitObject; }
    }

    public RotateEventArgs(Touch[] trackedFingers, ERotation rotation, float angle, GameObject hitObject) {
        this._trackedFingers = trackedFingers;
        this._rotation = rotation;
        this._angle = angle;
        this._hitObject = hitObject;
    }
}
