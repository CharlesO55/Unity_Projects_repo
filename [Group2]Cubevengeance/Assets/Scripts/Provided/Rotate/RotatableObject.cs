using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour {
    
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    [SerializeField]
    private float _speed = 30.0f;
    public float Speed {
        get { return _speed; }
    }

    [SerializeField]
    private ERotation _rotation;
    public ERotation Rotation {
        get { return _rotation; }
    }
}
