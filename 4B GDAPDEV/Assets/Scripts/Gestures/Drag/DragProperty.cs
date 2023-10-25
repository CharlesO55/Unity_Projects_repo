using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DragProperty
{
    [SerializeField] private float _minPressTime = 0.8f;
    public float MinPressTime
    {
        get { return _minPressTime; }
    }
}
