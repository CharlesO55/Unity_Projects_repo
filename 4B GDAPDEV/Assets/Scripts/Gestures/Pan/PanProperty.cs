using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PanProperty
{
    [Tooltip("Distance needed for fingers to pinch inwards")]
    [SerializeField]
    private float _maxDistance = 0.7f;
    public float MaxDistance
    {
        get { return _maxDistance; }
        set { _maxDistance = value; }
    }
}
