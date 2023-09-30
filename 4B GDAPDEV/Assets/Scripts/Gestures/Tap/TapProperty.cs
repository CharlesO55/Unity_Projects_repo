using System;
using UnityEngine;

[Serializable]
public class TapProperty
{
    [SerializeField] private float _time = 0.7f;
    public float TimeThreshold
    {
        get { return _time; }
        set { _time = value; }
    }


    [SerializeField] private float _maxDisance = 0.1f;
    public float MaxDisance
    {
        get { return _maxDisance; }
        set { _maxDisance = value; }
    }
}