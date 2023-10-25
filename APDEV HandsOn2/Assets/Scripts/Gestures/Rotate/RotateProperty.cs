using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                         DO NOT EDIT THIS SCRIPT                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

[Serializable]
public class RotateProperty {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    [Tooltip("Minimum distance between fingers to be a rotation.")]
    [SerializeField]
    private float _minDistance = 0.75f;
    public float MinDistance {
        get { return this._minDistance; }
        set { this._minDistance = value; }
    }

    [Tooltip("Minimum angle change to be a rotation.")]
    [SerializeField]
    private float _minRotationChange = 0.4f;
    public float MinRotationChange {
        get { return this._minRotationChange; }
        set { this._minRotationChange = value; }
    }
}
