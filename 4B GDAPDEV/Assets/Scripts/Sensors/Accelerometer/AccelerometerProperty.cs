using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AccelerometerProperty
{
    [SerializeField] private float _speedX = 30f;

    [SerializeField] private float _minChangeX = 0.2f;


    public float SpeedX { get { return _speedX; } }
    public float MinChangeX { get { return _minChangeX; } }
}
