using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleRotate : MonoBehaviour, IRotatable
{
    [SerializeField] float _rotateSpeed = 30;
    public void OnRotateInterface(RotateEventArgs args)
    {
        float angle = args.Angle * -this._rotateSpeed * Time.deltaTime;
        
        if(args.Direction == EnumRotateDirection.COUNTERCLOCKWSE)
        {
            angle = -angle;
        }

        this.transform.Rotate(0, 0, angle);
    }
}
