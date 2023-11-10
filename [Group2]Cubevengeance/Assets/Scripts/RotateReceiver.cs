using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateReceiver : MonoBehaviour {
    
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    [SerializeField]
    private List<RotatableObject> rotatableObjects;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void UpdateTimeScale(RotateEventArgs args) {
        /* * * * * * [TODO][3] * * * * * *
         * ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ */

        float currT = Time.timeScale;

        if (args.Rotation == ERotation.CLOCKWISE)
            currT += 0.05f;
        else
            currT -= 0.05f;

        currT = Mathf.Clamp(currT, 0f, 2f);
        Time.timeScale = currT;

        GUIManager.Instance.SetSpeed(Mathf.Ceil(currT * 100));
    }

    private void OnRotate(object sender, RotateEventArgs args) {
        this.UpdateTimeScale(args);

        foreach(RotatableObject rotatableObject in this.rotatableObjects) {
            float angle = args.Angle * rotatableObject.Speed * 0.01f;

            if(args.Rotation == ERotation.CLOCKWISE)
                angle = -angle;
            
            if(rotatableObject.Rotation == ERotation.COUNTERCLOCKWISE)
                angle = -angle;

            rotatableObject.transform.Rotate(0, 0, angle);
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                            LIFECYCLE METHODS                            *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Start() {
        GestureManager.Instance.OnRotate += this.OnRotate;
    }

    private void OnDisable() {
        GestureManager.Instance.OnRotate -= this.OnRotate;
    }
}
