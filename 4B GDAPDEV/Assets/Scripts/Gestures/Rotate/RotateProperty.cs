using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RotateProperty
{
    [SerializeField] private float _minDistance = 0.75f;
    [SerializeField] private float _minRotationChange = 0.4f;

    public float MinDistance
    {
        get { return _minDistance; }
    }
    public float MinRotationChange
    {
        get { return _minRotationChange; }
    }
}