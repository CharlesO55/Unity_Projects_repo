using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance;
    //private List<Touch> _trackedFingers;
    private Touch[] _trackedFingers;

    private float _gestureTime;
    private Vector2 _startPoint = Vector2.zero;
    private Vector2 _endPoint = Vector2.zero;

    [SerializeField]
    private TapProperty _tapProperty;
    public EventHandler<TapEventArgs> OnTap;

    [SerializeField]
    private SwipeProperty _swipeProperty;
    public EventHandler<SwipeEventArgs> OnSwipe;

    [SerializeField]
    private DragProperty _dragProperty;
    public EventHandler<DragEventArgs> OnDragDelegate;

    [SerializeField]
    private PanProperty _panProperty;
    public EventHandler<PanEventArgs> OnPanDelegate;

    [SerializeField]
    private SpreadProperty _spreadProperty;
    public EventHandler<SpreadEventArgs> OnSpreadDelegate;

    [SerializeField]
    private RotateProperty _rotateProperty;
    public EventHandler<RotateEventArgs> OnRotateDelegate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _trackedFingers = new Touch[2];
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Update()
    {
        switch (Input.touchCount)
        {
            case 1:
                this.CheckSingleFingerInput();
                break;
            case 2:
                this.CheckDualFingerInput();
                break;
            default:
                break;
        }
    }


    private bool TryGetObjHitByRaycast(Vector2 position, out GameObject objHit)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            objHit = hit.collider.gameObject;
            return true;
        }
        objHit = null;
        return false;
    }






    private void CheckSingleFingerInput()
    {
        this._trackedFingers[0] = Input.GetTouch(0);
        switch (this._trackedFingers[0].phase)
        {
            case TouchPhase.Began:
                this._gestureTime = 0f;
                this._startPoint = this._trackedFingers[0].position;
                break;
            case TouchPhase.Ended:
                this._endPoint = this._trackedFingers[0].position;

                this.CheckTap();
                this.CheckSwipe();

                break;
            default:
                this._gestureTime += Time.deltaTime;
                this.CheckDrag();
                break;
        }
    }

    void CheckTap()
    {
        if ((this._gestureTime <= this._tapProperty.TimeThreshold) &&
                            (Vector2.Distance(this._startPoint, this._endPoint) <= Screen.dpi * this._tapProperty.MaxDisance))
        {
            FireTapEvent();
        }
    }
    private void FireTapEvent()
    {
        /*if (OnTap != null)
        {*/
        /*    GameObject hitObject = null;
            Ray ray = Camera.main.ScreenPointToRay(this._startPoint);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitObject = hit.collider.gameObject;
                Debug.Log("[HIT]: " + hit.collider.name);
            }


            TapEventArgs args = new TapEventArgs(this._startPoint);
            this.OnTap(this, args);

            if (hitObject != null)
            {
                Debug.Log("[Checking if hit has tappable comp]...");
                ITapable handler = hitObject.GetComponent<ITapable>();
                if(handler != null)
                {
                    Debug.Log("[Found tappable]...");
                    args = new TapEventArgs(this._startPoint, hitObject);
                    handler.OnTap(args);
                }
            }
        //}
        this.OnTap?.Invoke(this, args);*/


        //STUFF TO PASS
        TapEventArgs args = new TapEventArgs(this._startPoint, null);

        //[1] FOR DIRECTED TARGETTED TAPS
        if (this.TryGetObjHitByRaycast(this._startPoint, out GameObject hitObj))
        {
            Debug.Log("Hit object");
            if (hitObj.TryGetComponent<ITapable>(out ITapable tappableScript))
            {
                Debug.Log("Found tappable comp");
                args.HitObject = hitObj;
                tappableScript.OnTap(args);
            }
        }

        //[2] TRIGGER ALL SUBSCRIBED FUNCTIONS TO THIS DELEGATE
        this.OnTap?.Invoke(this, args);
    }

    void CheckSwipe()
    {
        if (this._gestureTime <= this._swipeProperty.Time &&
            Vector2.Distance(this._startPoint, this._endPoint) >= (Screen.dpi * _swipeProperty.MinDistance))
        {
            this.FireSwipeEvent();
        }
    }

    void FireSwipeEvent()
    {

        Debug.Log("Fire swipe event");

        GameObject hitObject = null;


        Ray ray = Camera.main.ScreenPointToRay(this._startPoint);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            hitObject = hit.collider.gameObject;
            Debug.Log("[HIT]: " + hit.collider.name);
        }

        Vector2 rawDirection = this._endPoint - this._startPoint;
        ESwipeDirection direction = this.GetSwipeDirection(rawDirection);

        SwipeEventArgs args = new SwipeEventArgs(direction, rawDirection, this._startPoint, this._swipeProperty.MoveType, hitObject);
        //this.OnSwipe(this, args);

        if (hitObject != null)
        {
            ISwipeable handler = hitObject.GetComponent<ISwipeable>();
            if (handler != null)
            {
                handler.OnSwipe(args);
            }
        }

        this.OnSwipe?.Invoke(this, args);
    }

    ESwipeDirection GetSwipeDirection(Vector2 rawDirection)
    {
        if (Mathf.Abs(rawDirection.x) > Mathf.Abs(rawDirection.y))
        {
            if (rawDirection.x > 0)
            {
                Debug.Log("SWIPE RIGHT");
                return ESwipeDirection.RIGHT;
            }
            else
            {
                Debug.Log("SWIPE LEFT");
                return ESwipeDirection.LEFT;
            }
        }
        else
        {
            if (rawDirection.y > 0)
            {
                Debug.Log("SWIPE UP");
                return ESwipeDirection.UP;
            }
            else
            {
                Debug.Log("SWIPE DOWN");
                return ESwipeDirection.DOWN;
            }
        }
    }

    private void CheckDrag()
    {
        bool bTimeCheck = this._gestureTime > this._dragProperty.MinPressTime;


        if (bTimeCheck)
        {
            this.FireDragEvent();
        }
    }
    private void FireDragEvent()
    {
        DragEventArgs args = new DragEventArgs(this._trackedFingers[0]);

        //SWITCH start_position to tracked_finger current position for position check
        if (this.TryGetObjHitByRaycast(this._trackedFingers[0].position, out GameObject objHit))
        {
            if (objHit.TryGetComponent<IDraggable>(out IDraggable draggableScript))
            {
                args.ObjHit = objHit;
                draggableScript.OnDragInterface(args);
            }
        }


        this.OnDragDelegate?.Invoke(this, args);
    }


    private void CheckDualFingerInput()
    {
        this._trackedFingers[0] = Input.GetTouch(0);
        this._trackedFingers[1] = Input.GetTouch(1);


        switch (this._trackedFingers[0].phase, this._trackedFingers[1].phase)
        {
            case (TouchPhase.Moved, TouchPhase.Moved):
                this.CheckPan();
                break;
        }
        switch (this._trackedFingers[0].phase, this._trackedFingers[1].phase)
        {
            case (TouchPhase.Moved, _):
            case (_, TouchPhase.Moved):
                this.CheckSpread();
                this.CheckRotate();
                break;
        }
    }


    private void CheckPan()
    {
        float distanceApart = Vector2.Distance(this._trackedFingers[0].position, this._trackedFingers[1].position);
        //Debug.Log("Distance apart" + distanceApart + " Screen dpi" + Screen.dpi);

        if (distanceApart < this._panProperty.MaxDistance * Screen.dpi) {
            this.FirePanEvent();
        }
    }

    private void FirePanEvent()
    {
        Debug.Log("Fired Pan Event");


        this.OnPanDelegate?.Invoke(this, new PanEventArgs(this._trackedFingers));
    }

    private void CheckSpread()
    {
        float currDiff = Vector2.Distance(this._trackedFingers[0].position, this._trackedFingers[1].position);
        float prevDiff = Vector2.Distance(GetPreviousPoint(this._trackedFingers[0]), GetPreviousPoint(this._trackedFingers[1]));

        float change = currDiff - prevDiff;

        if (MathF.Abs(change) > this._spreadProperty.MinDistanceChange)
        {
            this.FireSpreadEvent(change);
        }
    }

    private void FireSpreadEvent(float distance){
        Debug.Log("spread event");
        
        SpreadEventArgs args = new SpreadEventArgs(this._trackedFingers, distance);

        Vector2 midpoint = this.GetMidpoint(this._trackedFingers[0].position, this._trackedFingers[1].position);

        //GET THE HIT OBJ
        if(this.TryGetObjHitByRaycast(midpoint, out GameObject hitObj))
        {
            args.HitObject = hitObj;
            if(hitObj.TryGetComponent<ISpreadable>(out ISpreadable interfaceScript))
            {
                interfaceScript.OnSpreadInterface(args);
            }
        }

        //INVOKE
        this.OnSpreadDelegate?.Invoke(this, args);
    }

    private void CheckRotate()
    {

        Vector2 prevPos = this.GetPreviousPoint(this._trackedFingers[0]) - this.GetPreviousPoint(this._trackedFingers[1]);
        Vector2 currPos = this._trackedFingers[0].position - this._trackedFingers[1].position;


        float angle = Vector2.Angle(prevPos, currPos);

        bool bDistanceMoved = Vector2.Distance(_trackedFingers[0].position, _trackedFingers[1].position) > _rotateProperty.MinDistance * Screen.dpi;
        bool bAngle = angle > _rotateProperty.MinRotationChange;

        if (bDistanceMoved && bAngle) 
        { 
            this.FireRotateEvent(angle, prevPos, currPos);
        }
    }

    private void FireRotateEvent(float angle, Vector2 prevDiff, Vector2 currDiff)
    {
        Debug.Log("rotate event");
        Vector3 cross = Vector3.Cross(prevDiff, currDiff);

        EnumRotateDirection eDir;

        
        if(cross.z > 0)
        {
            eDir = EnumRotateDirection.COUNTERCLOCKWSE;
        }
        else
        {
            eDir = EnumRotateDirection.CLOCKWISE;
        }

        RotateEventArgs args = new RotateEventArgs(_trackedFingers, eDir, cross.magnitude, angle);

        GameObject hitObj;
        if (this.TryGetObjHitByRaycast(this.GetMidpoint(_trackedFingers[0].position, _trackedFingers[1].position), out hitObj))
        {
            args.HitObject = hitObj;
            if (hitObj.TryGetComponent<IRotatable>(out IRotatable interfaceScript))
            {
                interfaceScript.OnRotateInterface(args);
            }
        }

        this.OnRotateDelegate?.Invoke(this, args);
    }


    /*private Vector2 GetMidpoint(Vector2[] points)
    {
        int activeFingies = Input.touchCount;

        //GET THE MIDPOINT
        Vector2 midpoint = Vector2.zero;
        for (int i = 0; i < activeFingies; i++)
        {
            midpoint += _trackedFingers[i].position;
        }
        midpoint /= activeFingies;

        return midpoint;
    }*/

    private Vector2 GetMidpoint(Vector2 point1, Vector2 point2)
    {
        return (point1 + point2) / 2;
    }

    private Vector2 GetPreviousPoint(Touch finger)
    {
        return finger.position - finger.deltaPosition;
    }
}