using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*.............................................................................*
 |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
 |                                INSTRUCTIONS                                 |
 |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
 |'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''|
 | You are free to alter this script as you see fit. Just make sure that the   |
 | resulting script still acts in accordance with what was discussed in class. |
 *'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''*/

public class RotateEventArgs : EventArgs 
{
    public ERotateDirection Direction;
    public Vector3 RotationAngle;
    
    //Not needed anymore since we directly access IRotatable interface instead
    public GameObject HitObj;

    public RotateEventArgs(ERotateDirection direction, Vector3 rotationAngle, GameObject hitObj = null)
    {
        Direction = direction;
        RotationAngle = rotationAngle;
        HitObj = hitObj;
    }
}
