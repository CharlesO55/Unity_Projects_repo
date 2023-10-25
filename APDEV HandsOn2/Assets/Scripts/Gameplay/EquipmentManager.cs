using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                         DO NOT EDIT THIS SCRIPT                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class EquipmentManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static EquipmentManager Instance;
    private AudioSource _source;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void PlayClick() {
        this._source.Play();
    }

    private void ShootRay() {
        GameObject hitObject = null;
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, ~LayerMask.GetMask("Sensor Receiver")))
            hitObject = hit.collider.gameObject;

        if(hitObject != null) {
            // Debug.Log("Hit Object " + hitObject.name);
            GhostCollider handler = hitObject.GetComponent<GhostCollider>();
            if(handler != null)
                this.UpdateEquipment(handler);
        }
        else {
            GUIManager.Instance.UpdateEquipment(EColliderLevel.LEVEL_1);
            if(GhostManager.Instance.GetGhost() != null)
                GhostManager.Instance.GetGhost().IsIncreaseAlpha = false;
        }
    }

    private void UpdateEquipment(GhostCollider ghostCollider) {
        GUIManager.Instance.UpdateEquipment(ghostCollider.Level);

        switch(ghostCollider.Level) {
            case EColliderLevel.LEVEL_4:
                GhostManager.Instance.GetGhost().IncreaseAlpha();
                GhostManager.Instance.GetGhost().IncreaseAlpha();
                break;

            default:
                GhostManager.Instance.GetGhost().IsIncreaseAlpha = false;
                break;
        }
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
        this._source = this.GetComponent<AudioSource>();
    }

    private void Update() {
        if(GUIManager.Instance.IsOpen &&
           GUIManager.Instance.EquipmentType == EEquipmentType.EMF &&
           GhostManager.Instance.GetGhost() != null && !GhostManager.Instance.GetGhost().IsTriggerHunt) {
            this.ShootRay();
        }
    }
}
