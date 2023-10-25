using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                         DO NOT EDIT THIS SCRIPT                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

[Serializable]
public class SpreadProperty {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    [Tooltip("Minimum distance change between fingers to be a spread.")]
    [SerializeField]
    private float _minDistanceChange = 0.5f;
    public float MinDistanceChange {
        get { return this._minDistanceChange; }
        set { this._minDistanceChange = value; }
    }
}
