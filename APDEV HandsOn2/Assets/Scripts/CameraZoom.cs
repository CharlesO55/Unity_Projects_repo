using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float _fMaxZoom = 95;
    private float _fMinZoom = 80;

    DeviceOrientation _prevOrientation;
    
    private void Start()
    {
        GestureManager.Instance.OnSpread += this.ZoomCamera;
    }

    private void OnDestroy()
    {
        GestureManager.Instance.OnSpread -= this.ZoomCamera;
    }


    private void Update()
    {
        /*switch (this._prevOrientation, Input.deviceOrientation)
        {
            //case (DeviceOrientation.LandscapeLeft, ~DeviceOrientation.LandscapeRight))):
            case (DeviceOrientation.LandscapeRight, DeviceOrientation.Portrait):
            case (DeviceOrientation.LandscapeRight, DeviceOrientation.PortraitUpsideDown):
            case (DeviceOrientation.LandscapeLeft, DeviceOrientation.Portrait):
            case (DeviceOrientation.LandscapeLeft, DeviceOrientation.PortraitUpsideDown):
                GestureManager.Instance.OnSpread -= this.ZoomCamera;
                break;

            case (DeviceOrientation.Portrait, DeviceOrientation.LandscapeLeft):
            case (DeviceOrientation.Portrait, DeviceOrientation.LandscapeRight):
            case (DeviceOrientation.PortraitUpsideDown, DeviceOrientation.LandscapeLeft):
            case (DeviceOrientation.PortraitUpsideDown, DeviceOrientation.LandscapeRight):
                GestureManager.Instance.OnSpread -= this.ZoomCamera;
                break;
        }*/
        
        _prevOrientation = Input.deviceOrientation;
    }

    private void ZoomCamera(object sender, SpreadEventArgs args)
    {
        Debug.Log(Input.deviceOrientation);


        if(!GUIManager.Instance.IsOpen &&
           (Input.deviceOrientation == DeviceOrientation.LandscapeRight ||
           Input.deviceOrientation == DeviceOrientation.LandscapeLeft))
        {
            Debug.Log("Zoom");

            float fov = Camera.main.fieldOfView;
            fov += -args.DistanceDelta * Time.deltaTime;

            Camera.main.fieldOfView = Mathf.Clamp(fov, this._fMinZoom, this._fMaxZoom);
        }
    }
}
