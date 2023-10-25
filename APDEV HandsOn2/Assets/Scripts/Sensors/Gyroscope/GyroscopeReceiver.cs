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
            /* * * * * * [TODO][1] * * * * * *
             |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
             | Fill this script up with the  |
             | necessary logic. Refer to the |
             | document for more details.    |
             |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
             * * * * * * * * * * * * * * * * */
            
            //Debug.Log("gyroooooonimo");
            Vector3 _rot = Input.gyro.rotationRate;
            
            /*Quaternion _rot = Input.gyro.attitude;
            _rot = new Quaternion(_rot.x, _rot.y, -_rot.z, -_rot.w);
*/
            //this.transform.rotation = _rot;
            this.transform.Rotate(new Vector3(-_rot.x, -_rot.y, 0));

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
