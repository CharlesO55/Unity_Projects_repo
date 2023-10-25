using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                         DO NOT EDIT THIS SCRIPT                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class Ghost : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private AudioSource _source;
    private GyroscopeReceiver _gyroscopeReceiver;

    private float _speed = 60.0f;
    private bool _isIncreaseAlpha = false;
    public bool IsIncreaseAlpha {
        get { return this._isIncreaseAlpha; }
        set { this._isIncreaseAlpha = value; }
    }
    private float _rateAlphaIncrease = 0.01f;
    private float _timeAlphaDecay = 0.0f;
    private float _thresholdAlphaDecay = 0.2f;
    private float _rateAlphaDecay = 0.05f;

    private float _timeDespawnDelay = 0.0f;
    private float _thresholdDespawnDelay = 2.3f;

    private float _huntDistance = 1.5f;
    private float _offsetY = -2.3f;

    [SerializeField]
    private Transluscify _ghostTransluscify;
    public Transluscify GhostTransluscify {
        get { return this._ghostTransluscify; }
    }

    [SerializeField]
    private bool _isTriggerHunt = false;
    public bool IsTriggerHunt {
        get { return this._isTriggerHunt; }
        set { this._isTriggerHunt = value; }
    }

    [SerializeField]
    private List<AudioClip> _clips;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void CheckHunt() {
        if(this._ghostTransluscify.Alpha == 1.0f) {
            this._isTriggerHunt = true;
            this._gyroscopeReceiver.Pause();
            this._source.Play();
            GUIManager.Instance.UpdateEquipment(EColliderLevel.LEVEL_5);
        }
    }

    private bool Hunt() {
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * _huntDistance;
        targetPosition.y += this._offsetY;
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, this._speed * Time.deltaTime);

        if(this.transform.position == targetPosition)
            return true;
        return false;
    }

    public void IncreaseAlpha() {
        this._isIncreaseAlpha = true;

        if((this._ghostTransluscify.Alpha + this._rateAlphaIncrease) > 1.0f)
            this._ghostTransluscify.Alpha = 1.0f;
        else
            this._ghostTransluscify.Alpha += this._rateAlphaIncrease;
    }

    public void DecreaseAlpha() {
        this._isIncreaseAlpha = false;

        if((this._ghostTransluscify.Alpha - this._rateAlphaIncrease) < 0.0f)
            this._ghostTransluscify.Alpha = 0.0f;
        else
            this._ghostTransluscify.Alpha -= this._rateAlphaIncrease;
    }

    private void UpdateAlphaDecay() {
        if(this._timeAlphaDecay >= this._thresholdAlphaDecay) {
            this._timeAlphaDecay = 0.0f;
            this._ghostTransluscify.Alpha -= this._rateAlphaDecay;
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                            LIFECYCLE METHODS                            *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Start() {
        this._ghostTransluscify.Alpha = 0.0f;
        this._gyroscopeReceiver = FindObjectOfType<GyroscopeReceiver>();
        this._source = this.GetComponent<AudioSource>();

        int clipIndex = Random.Range(0, this._clips.Count);
        this._source.clip = this._clips[clipIndex];
    }

    private void Update() {
        if(this._isTriggerHunt) {
            if(this.Hunt()) {
                this._timeDespawnDelay += Time.deltaTime;
                if(this._timeDespawnDelay >= this._thresholdDespawnDelay) {
                    this._gyroscopeReceiver.Play();
                    if(GUIManager.Instance.IsOpen)
                        GUIManager.Instance.UpdateEquipment(EColliderLevel.LEVEL_1);
                    Destroy(this.gameObject);
                }
            }
        }

        else {
            this.CheckHunt();
            if(!GUIManager.Instance.IsOpen)
                this.UpdateAlphaDecay();
            this._timeAlphaDecay += Time.deltaTime;
        }
    }
}
