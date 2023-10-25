using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerReceiver : MonoBehaviour
{
    [SerializeField] private AccelerometerProperty _accProperty;

    private void CheckAccelerometer() 
    {
        if(Mathf.Abs(Input.acceleration.x) > _accProperty.MinChangeX)
        {
            this.FireAccelerometerEvent();
        }

        Debug.Log(Input.acceleration.x);
    }
    private void FireAccelerometerEvent() 
    {
        Debug.Log("acclerate");

        Vector3 deltaTrans = new Vector3(Input.acceleration.x, 0, 0) * Time.deltaTime;
        
        deltaTrans.x *= _accProperty.SpeedX;

        this.transform.Translate(deltaTrans);
        //this.transform.position += deltaTrans;
    }


    private void FixedUpdate() 
    {
        this.CheckAccelerometer();
    }
}
