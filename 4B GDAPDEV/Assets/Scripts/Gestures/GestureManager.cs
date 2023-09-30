using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance;
    private Touch _trackedFinger;
    private float _gestureTime;
    private Vector2 _tapStartPoint = Vector2.zero;
    private Vector2 _tapEndPoint = Vector2.zero;

    [SerializeField]
    private TapProperty _tapProperty;
    public EventHandler<TapEventArgs> OnTap;


    private void FireTapEvent(Vector2 tapPosition)
    {
        Debug.Log("Tap");

        this.OnTap(this, new TapEventArgs(tapPosition));
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

    void Update()
    {
        if (Input.touchCount > 0)
        {
            this._trackedFinger = Input.GetTouch(0);
            switch (this._trackedFinger.phase)
            {
                case TouchPhase.Began:
                    this._gestureTime = 0f;
                    this._tapStartPoint = this._trackedFinger.position;
                    break;
                case TouchPhase.Ended:
                    this._tapEndPoint = this._trackedFinger.position;

                    if ((this._gestureTime <= this._tapProperty.TimeThreshold) &&
                        (Vector2.Distance(this._tapStartPoint, this._tapEndPoint) <= Screen.dpi * this._tapProperty.MaxDisance))
                    {
                        FireTapEvent(this._trackedFinger.position);
                    }
                    
                    break;
                default:
                    this._gestureTime += Time.deltaTime;
                    break;
            }
        }
    }
}
