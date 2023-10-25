using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*.............................................................................*
 |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
 |                                INSTRUCTIONS                                 |
 |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
 |'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''|
 | This script has INCOMPLETE code. Use [CTRL][F] to look for the [TODO] tags. |
 *'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''*/

public class RotateReceiver : MonoBehaviour, IRotatable {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private float _speedRotate = 30;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void OnRotate(RotateEventArgs args) {
        /* * * * * * [TODO][2] * * * * * *
         |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
         | Fill this script up with the  |
         | necessary logic. Refer to the |
         | document for more details.    |
         |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
         * * * * * * * * * * * * * * * * */

        /*if (args.HitObj != this.gameObject)
        {
            return;
        }*/

        
        this.transform.Rotate(args.RotationAngle * Time.deltaTime * this._speedRotate);

        if(args.Direction == ERotateDirection.CLOCKWISE && GUIManager.Instance.IsOpen)
        {
            Debug.Log("Close EMF");
            GUIManager.Instance.Close();
        }
        else if(args.Direction == ERotateDirection.COUNTERCLOCKWISE && !GUIManager.Instance.IsOpen) 
        {
            Debug.Log("Open EMF");
            GUIManager.Instance.OpenEMF();
        }
    }
}
