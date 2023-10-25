using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationLock : MonoBehaviour
{
    public static OrientationLock Instance;
    private DeviceOrientation _prevDeviceOrientation;

    private bool _isLocked = false;
    public bool IsLocked
    {
        get { return _isLocked; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        this._prevDeviceOrientation = Input.deviceOrientation;
    }

    public void Unlock()
    {
        Debug.Log("Unlock");
        this._isLocked = false;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    public void Lock()
    {
        Debug.Log("Lock");
        this._isLocked = true;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;


        int orientation = (int)this._prevDeviceOrientation;

        if(orientation > 4) 
        {
            orientation = (int)DeviceOrientation.LandscapeLeft;
        }

        Screen.orientation = (ScreenOrientation)orientation;
    }

}
