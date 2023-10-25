using System;
using UnityEngine;

[Serializable]
public class SwipeProperty
{
    [SerializeField]
    private float _time = 2.0f;
    public float Time { 
        get { return _time; } 
        set { _time = value; }
    }

    [SerializeField]
    private float _minDistance = 0.7f;
    public float MinDistance
    {
        get { return _minDistance; }
        set { _minDistance = value; }
    }


    [Range(0, 1)]
    [SerializeField]
    private int _moveType = 0;
    public int MoveType{   
        get { return _moveType; }
        set { _moveType = value; } 
    }
}
