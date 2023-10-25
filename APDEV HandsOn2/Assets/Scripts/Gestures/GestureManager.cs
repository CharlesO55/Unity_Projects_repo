using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*.............................................................................*
 |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
 |                                INSTRUCTIONS                                 |
 |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
 |'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''|
 | This script has INCOMPLETE code. Use [CTRL][F] to look for the [TODO] tags. |
 *'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''*/

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
    private TapProperty _tapProperty;
    public EventHandler<TapEventArgs> OnTap;

    [SerializeField]
    private SpreadProperty _spreadProperty;
    public EventHandler<SpreadEventArgs> OnSpread;

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
                this.CheckTap();
                break;

            default:
                this._gestureTime += Time.deltaTime;
                break;
        }
    }

    private void CheckTap() {
        if(this._gestureTime <= this._tapProperty.Time &&
           Vector2.Distance(this._startPoint, this._endPoint) <= (Screen.dpi * this._tapProperty.MaxDistance)) {
            this.FireTapEvent();
        }
    }

    private void FireTapEvent() {
        GameObject hitObject = this.GetHitGraphic(this._startPoint);
        if(hitObject == null)
            hitObject = this.GetHitObject(this._startPoint);

        TapEventArgs args = new TapEventArgs();

        if(hitObject != null) {
            ITappable handler = hitObject.GetComponent<ITappable>();
            if(handler != null) {
                handler.OnTap(args);
            }
        }
    }

    private void CheckDualFingerInput() {
        this._trackedFingers[0] = Input.GetTouch(0);
        this._trackedFingers[1] = Input.GetTouch(1);

        switch(this._trackedFingers[0].phase, this._trackedFingers[1].phase) {
            case (TouchPhase.Moved, _):
            case (_, TouchPhase.Moved):
                this.CheckSpread();
                this.CheckRotate();
                break;
        }
    }


    private void CheckSpread() {

        /* * * * * * [TODO][3] * * * * * *
         |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
         | Fill this script up with the  |
         | necessary logic. Refer to the |
         | document for more details.    |
         |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
         * * * * * * * * * * * * * * * * */

        /* [NOTE] : Uncomment and complete the lines below. */


        float previousDistance = /*[TODO]*/Vector2.Distance(GetPreviousPoint(this._trackedFingers[0]), GetPreviousPoint(this._trackedFingers[1]));
        float currentDistance = /*[TODO]*/ Vector2.Distance(this._trackedFingers[0].position, this._trackedFingers[1].position);
        
        if (Mathf.Abs(currentDistance - previousDistance) >= (this._spreadProperty.MinDistanceChange))
        {
            this.FireSpreadEvent(currentDistance - previousDistance);
        }

    }

    private void FireSpreadEvent(float distanceDelta) {

        /* * * * * * [TODO][3] * * * * * *
         |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
         | Fill this script up with the  |
         | necessary logic. Refer to the |
         | document for more details.    |
         |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
         * * * * * * * * * * * * * * * * */

        Debug.Log("Spread event");

        SpreadEventArgs args = new SpreadEventArgs(distanceDelta);
        //Doesn't matter what spread collides with
        

        this.OnSpread?.Invoke(this, args);
    }

    private void CheckRotate() {

        /* * * * * * [TODO][2] * * * * * *
         |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
         | Fill this script up with the  |
         | necessary logic. Refer to the |
         | document for more details.    |
         |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
         * * * * * * * * * * * * * * * * */

        /* [NOTE] : Uncomment and complete the lines below. */


        Vector2 previousDifference = /*[TODO]*/ GetPreviousPoint(this._trackedFingers[1]) - GetPreviousPoint(this._trackedFingers[0]);
        Vector2 currentDifference = /*[TODO]*/ this._trackedFingers[1].position - this._trackedFingers[0].position;
        float angle = Vector2.Angle(previousDifference, currentDifference);


        bool bAngleCheck = angle >= this._rotateProperty.MinRotationChange;
        bool bMinDistanceCheck = Vector2.Distance(this._trackedFingers[0].position, this._trackedFingers[1].position) >= this._rotateProperty.MinDistance * Screen.dpi;


        if (/*[TODO]*/bAngleCheck && bMinDistanceCheck)
        {
            this.FireRotateEvent(angle, previousDifference, currentDifference);
        }

    }

    private void FireRotateEvent(float angle, Vector2 previousDifference, Vector2 currentDifference) {

        /* * * * * * [TODO][2] * * * * * *
         |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
         | Fill this script up with the  |
         | necessary logic. Refer to the |
         | document for more details.    |
         |~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~|
         * * * * * * * * * * * * * * * * */
        Debug.Log("Rotate");


        GameObject hitObject = null;
        Vector3 cross = Vector3.Cross(previousDifference, currentDifference);
        ERotateDirection direction;

        /* * * [TODO] * * */


        Debug.Log("Mag" + cross.z);
        if (cross.z > 0)
        {
            direction = ERotateDirection.CLOCKWISE;
            Debug.Log("CW");
        }
        else
        {
            direction = ERotateDirection.COUNTERCLOCKWISE;
            Debug.Log("CCW");
        }

        RotateEventArgs args = new RotateEventArgs(direction, cross, null);

        Vector2 midPoint = GetMidPoint(this._trackedFingers[0].position, this._trackedFingers[1].position);
        GameObject hitObj = this.GetHitObject(midPoint);


        if (hitObj != null)
        {
            args.HitObj = this.gameObject;
            if (hitObj.TryGetComponent<IRotatable>(out IRotatable interfaceScript))
            {
                interfaceScript.OnRotate(args);
            }
        }

        //Technically not needed since RotateReceiver is directly called and not suscribed but kekw
        this.OnRotate?.Invoke(this, args);

    }

    private GameObject GetHitGraphic(Vector2 screenPoint) {
        PointerEventData pointerEventData = new PointerEventData(this._eventSystem);
        pointerEventData.position = screenPoint;

        List<RaycastResult> hits = new List<RaycastResult>();
        this._graphicRaycaster.Raycast(pointerEventData, hits);

        if(hits.Count > 0)
            return hits[0].gameObject;

        return null;
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
