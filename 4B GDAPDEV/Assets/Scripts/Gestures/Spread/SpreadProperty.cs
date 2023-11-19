using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpreadProperty
{
    [Tooltip("Distance needed for fingers to spread outwards")]
    [SerializeField]
    private float _minDistanceChange = 0.5f;
    public float MinDistanceChange
    {
        get { return _minDistanceChange; }
        set { _minDistanceChange = value; }
    }
}
