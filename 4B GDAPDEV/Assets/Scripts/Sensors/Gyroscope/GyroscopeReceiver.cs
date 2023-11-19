using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeReceiver : MonoBehaviour
{
    void Start()
    {
        this.EnableGyroscope();   
    }

    void FixedUpdate()
    {
        this.CheckGyroscope();
    }

    void EnableGyroscope()
    {
        if(SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogError("Device has no Gyroscope");
        }
    }

    void CheckGyroscope()
    {
        if (Input.gyro.enabled)
        {
            this.FireGyroscopeEvent();
        }
    }

    void FireGyroscopeEvent()
    {
        Vector3 rotation = Input.gyro.rotationRate;

        rotation.x *= -1;
        rotation.y *= -1;

        this.transform.Rotate(rotation);
    }
}
