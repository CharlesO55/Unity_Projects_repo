using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeReceiver : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private bool _isRunning = true;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void EnableGyroscope() {
        if(SystemInfo.supportsGyroscope) {
            Input.gyro.enabled = true;
        }
        else {
            Debug.LogError("Device has no Gyroscope.");
        }
    }

    private void CheckGyroscope() {
        if(Input.gyro.enabled) {
            this.FireGyroscope();
        }
    }

    private void FireGyroscope() {
        if(this._isRunning) {
            Vector3 rotation = Input.gyro.rotationRate;

            rotation.x = 0.0f;
            rotation.y = -rotation.y;

            this.transform.Rotate(rotation);
        }
    }

    public void Play() {
        this._isRunning = true;
    }

    public void Pause() {
        this._isRunning = false;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                            LIFECYCLE METHODS                            *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Start() {
        this.EnableGyroscope();
    }

    public void FixedUpdate() {
        this.CheckGyroscope();
    }
}
