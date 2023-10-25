using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                         DO NOT EDIT THIS SCRIPT                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

[Serializable]
public class TapProperty {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    [Tooltip("Maximum allowable time to be considered as a tap.")]
    [SerializeField]
    float _time = 0.7f;
    public float Time {
        get { return this._time; }
        set { this._time = value; }
    }
    
    [Tooltip("Maximum allowable distance to be considered as a tap.")]
    [SerializeField]
    float _maxDistance = 0.1f;
    public float MaxDistance {
        get { return this._maxDistance; }
        set { this._maxDistance = value; }
    }
}
