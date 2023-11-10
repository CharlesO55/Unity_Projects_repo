using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GestureManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static GestureManager Instance;

    private Touch[] _trackedFingers = new Touch[2];
    private float _gestureTime = 0;
    private Vector2 _startPoint = Vector2.zero;
    private Vector2 _endPoint = Vector2.zero;

    [SerializeField]
    private GraphicRaycaster _graphicRaycaster;

    [SerializeField]
    private EventSystem _eventSystem;

    [SerializeField]
    private SwipeProperty _swipeProperty;
    public EventHandler<SwipeEventArgs> OnSwipe;

    [SerializeField]
    private RotateProperty _rotateProperty;
    public EventHandler<RotateEventArgs> OnRotate;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void CheckSingleFingerInput() {
        this._trackedFingers[0] = Input.GetTouch(0);

        switch(this._trackedFingers[0].phase) {
            case TouchPhase.Began:
                this._startPoint = this._trackedFingers[0].position;
                this._gestureTime = 0;
                break;

            case TouchPhase.Ended:
                this._endPoint = this._trackedFingers[0].position;
                this.CheckSwipe();
                break;

            default:
                this._gestureTime += Time.deltaTime;
                break;
        }
    }

    private void CheckDualFingerInput() {
        this._trackedFingers[0] = Input.GetTouch(0);
        this._trackedFingers[1] = Input.GetTouch(1);

        switch(this._trackedFingers[0].phase, this._trackedFingers[1].phase) {
            case (TouchPhase.Moved, _):
            case (_, TouchPhase.Moved):
                this.CheckRotate();
                break;
        }
    }

    private void CheckSwipe() {
        if(this._gestureTime <= this._swipeProperty.Time &&
            Vector2.Distance(this._startPoint, this._endPoint) >= (Screen.dpi * this._swipeProperty.MinDistance)) {
            this.FireSwipeEvent();
        }
    }

    private void FireSwipeEvent() {
        SwipeEventArgs args = new SwipeEventArgs();

        if(this.OnSwipe != null) {
            this.OnSwipe(this, args);
        }
    }

    private void CheckRotate() {
        Vector2 previousPoint0 = this.GetPreviousPoint(this._trackedFingers[0]);
        Vector2 previousPoint1 = this.GetPreviousPoint(this._trackedFingers[1]);

        Vector2 previousDifference = previousPoint0 - previousPoint1;
        Vector2 currentDifference = this._trackedFingers[0].position - this._trackedFingers[1].position;

        float angle = Vector2.Angle(previousDifference, currentDifference);

        if(Vector2.Distance(this._trackedFingers[0].position, this._trackedFingers[1].position) >=
           (Screen.dpi * this._rotateProperty.MinDistance) &&
           angle >= this._rotateProperty.MinRotationChange) {
            this.FireRotateEvent(angle, previousDifference, currentDifference);
        }
    }

    private void FireRotateEvent(float angle, Vector2 previousDifference, Vector2 currentDifference) {
        Vector3 cross = Vector3.Cross(previousDifference, currentDifference);
        ERotation rotation;

        if(cross.z > 0)
            rotation = ERotation.COUNTERCLOCKWISE;
        else
            rotation = ERotation.CLOCKWISE;

        Vector2 midPoint = this.GetMidPoint(this._trackedFingers[0].position, this._trackedFingers[1].position);
        GameObject hitObject = this.GetHitObject(midPoint);

        RotateEventArgs args = new RotateEventArgs(this._trackedFingers, rotation, angle, hitObject);

        if(this.OnRotate != null) {
            this.OnRotate(this, args);
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                              HELPER METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private GameObject GetHitObject(Vector2 screenPoint) {
        GameObject hitObject = null;
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            hitObject = hit.collider.gameObject;

        return hitObject;
    }

    private ESwipeDirection GetSwipeDirection(Vector2 rawDirection) {
        if(Mathf.Abs(rawDirection.x) > Mathf.Abs(rawDirection.y)) {
            if(rawDirection.x > 0) {
                return ESwipeDirection.RIGHT;
            }

            else {
                return ESwipeDirection.LEFT;
            }
        }

        else {
            if(rawDirection.y > 0) {
                return ESwipeDirection.UP;
            }

            else {
                return ESwipeDirection.DOWN;
            }
        }
    }

    private Vector2 GetPreviousPoint(Touch finger) {
        return finger.position - finger.deltaPosition;
    }

    private Vector2 GetMidPoint(Vector2 pointA, Vector2 pointB) {
        return (pointA + pointB) / 2;
    }


    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                            LIFECYCLE METHODS                            *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Update() {
        if(Input.touchCount > 0) {
            switch(Input.touchCount) {
                case 1:
                    this.CheckSingleFingerInput();
                    break;

                case 2:
                    this.CheckDualFingerInput();
                    break;
            }
        }
    }
}
