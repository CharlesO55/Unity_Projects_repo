using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SensorSpawnReceiver : MonoBehaviour
{
    void Start()
    {
        this.EnableGyroscope();
    }



    // Update is called once per frame
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
            Debug.LogError("No gyro");
        }
    }

    void CheckGyroscope()
    {
        if (Input.gyro.enabled)
        {
            this.FireGyroscope();
        }
    }

    void FireGyroscope()
    {
        Quaternion rotation = /*Input.gyro.attitude;*/this.Convert(Input.gyro.attitude);
        this.transform.rotation = rotation;
    }

    Quaternion Convert(Quaternion q)
    {
        return new Quaternion(q.x , q.y, -q.z, -q.w);
    }
}
