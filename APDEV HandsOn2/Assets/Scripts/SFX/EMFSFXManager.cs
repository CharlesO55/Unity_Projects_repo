using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                         DO NOT EDIT THIS SCRIPT                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class EMFSFXManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static EMFSFXManager Instance;
    private AudioSource _source;

    [SerializeField]
    private List<AudioClip> _clips;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void TurnOff() {
        this.PlayLevel1();
        EquipmentManager.Instance.PlayClick();
    }

    public void PlayLevel1() {
        this._source.Stop();
    }

    public void PlayLevel2() {
        this._source.clip = this._clips[0];
        this._source.loop = true;
        if(!this._source.isPlaying)
            this._source.Play();
    }
    public void PlayLevel3() {
        this._source.clip = this._clips[1];
        this._source.loop = true;
        if(!this._source.isPlaying)
            this._source.Play();
    }

    public void PlayLevel4() {
        this._source.clip = this._clips[2];
        this._source.loop = true;
        if(!this._source.isPlaying)
            this._source.Play();
    }

    public void PlayLevel5() {
        this._source.clip = this._clips[3];
        this._source.loop = true;
        if(!this._source.isPlaying)
            this._source.Play();
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

    private void Start() {
        this._source = GetComponent<AudioSource>();
        this.PlayLevel1();
    }
}
