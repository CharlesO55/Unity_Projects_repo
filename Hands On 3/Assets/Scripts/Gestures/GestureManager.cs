using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |               You MAY edit this script, if needed.              |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class GestureManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static GestureManager Instance;
    private Touch[] _trackedFingers = new Touch[2];
    private float _gestureTime;
    private Vector2 _startPoint = Vector2.zero;
    private Vector2 _endPoint = Vector2.zero;
    private GameObject _hitObject = null;

    [SerializeField]
    private TapProperty _tapProperty;
    public EventHandler<TapEventArgs> OnTap;

    [SerializeField]
    private DragProperty _dragProperty;
    public EventHandler<DragEventArgs> OnDrag;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void CheckTap() {
        if(this._gestureTime <= this._tapProperty.Time &&
                       Vector2.Distance(this._startPoint, this._endPoint) < (Screen.dpi * this._tapProperty.MaxDistance)) {
            this.FireTapEvent();
        }
    }

    private void FireTapEvent() {
        GameObject hitObject = this.GetHitObject(this._startPoint);
        TapEventArgs args = new TapEventArgs();

        if(this.OnTap != null)
            this.OnTap(this, args);

        if(hitObject != null) {
            ITappable handler = hitObject.GetComponent<ITappable>();
            if(handler != null)
                handler.OnTap(args);
        }
    }

    private void CheckDrag() {
        if(this._gestureTime >= this._dragProperty.Time) {
            this.FireDragEEvent();
        }
    }

    private void FireDragEEvent() {
        GameObject hitObject = this.GetHitObject(this._trackedFingers[0].position);
        DragEventArgs args = new DragEventArgs(this._trackedFingers[0]);

        if(hitObject != null) {
            IDraggable handler = hitObject.GetComponent<IDraggable>();
            if(handler != null) {
                this._hitObject = hitObject;
                handler.OnDrag(args);

                if(hitObject.GetComponent<MinionManager>() != null || hitObject.GetComponent<CardMinionManager>() != null)
                    AudioManager.Instance.Play(EAudioId.CARD_DRAG);
            }
        }
    }

    private void CheckSingleFingerInput() {
        this._trackedFingers[0] = Input.GetTouch(0);

        switch(this._trackedFingers[0].phase) {
            case TouchPhase.Began:
                this._startPoint = this._trackedFingers[0].position;
                this._gestureTime = 0;
                break;

            case TouchPhase.Ended:
                this._endPoint = this._trackedFingers[0].position;
                this.CheckHits();
                this.CheckTap();
                break;

            default:
                this._gestureTime += Time.deltaTime;
                if(this._hitObject == null)
                    this.CheckDrag();
                else {
                    DragEventArgs args = new DragEventArgs(this._trackedFingers[0]);
                    IDraggable handler = this._hitObject.GetComponent<IDraggable>();
                    if(handler != null)
                        handler.OnDrag(args);
                }
                break;

        }
    }

    private void CheckHits() {
        bool hitMinion = false;

        if(this._hitObject != null) {
            MinionManager minion = this._hitObject.GetComponent<MinionManager>();
            if(minion != null) {
                minion.OnRelease(this._endPoint);
                hitMinion = minion.HitMinion;
            }

            CardMinionManager cardMinion = this._hitObject.GetComponent<CardMinionManager>();
            if(cardMinion != null) {
                cardMinion.OnRelease(this._endPoint);
            }

            this._hitObject = null;
        }

        bool playerResult = PlayerManager.Instance.RemoveDeadMinion();
        bool battlegroundResult = BattlegroundManager.Instance.RemoveDeadMinion();

        if(playerResult || battlegroundResult)
            AudioManager.Instance.Play(EAudioId.KILL_MINION);

        else if(hitMinion)
            AudioManager.Instance.Play(EAudioId.DAMAGE_MINION);
    }

    private GameObject GetHitObject(Vector2 screenPoint) {
        GameObject hitObject = null;
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, ~Settings.Instance.GestureIgnoredLayers)) {
            hitObject = hit.collider.gameObject;
        }

        return hitObject;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        LIFECYCLE METHODS                        |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void Update() {
        if(Input.touchCount > 0) {
            this.CheckSingleFingerInput();
        }
    }
}
