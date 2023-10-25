using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                         DO NOT EDIT THIS SCRIPT                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class Shutter : MonoBehaviour, ITappable {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private AudioSource _source;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void OnTap(TapEventArgs args) {
        this.ShootRay();
    }

    private void ShootRay() {
        this._source.Play();

        GameObject hitObject = null;
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, ~LayerMask.GetMask("Sensor Receiver")))
            hitObject = hit.collider.gameObject;

        if(hitObject != null) {
            GhostCollider handler = hitObject.GetComponent<GhostCollider>();
            if(handler != null)
                this.CheckShot(handler.Level);
        }
    }

    private void CheckShot(EColliderLevel level) {
        switch(level) {
            case EColliderLevel.LEVEL_5:
            case EColliderLevel.LEVEL_4:
                if(GhostManager.Instance.GetGhost() != null) {
                    if(GhostManager.Instance.GetGhost().GhostTransluscify.Alpha > 0.3f)
                        Destroy(GhostManager.Instance.GetGhost().gameObject);
                    else {
                        GhostManager.Instance.GetGhost().GhostTransluscify.Alpha = 1.0f;
                    }
                }
                break;

            case EColliderLevel.LEVEL_3:
                GhostManager.Instance.GetGhost().GhostTransluscify.Alpha = 1.0f;
                break;
        } 
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                            LIFECYCLE METHODS                            *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Start() {
        this._source = this.GetComponent<AudioSource>();
    }
}
