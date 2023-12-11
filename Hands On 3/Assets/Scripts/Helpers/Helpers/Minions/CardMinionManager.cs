using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |               You MAY edit this script, if needed.              |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class CardMinionManager : MonoBehaviour, IDraggable {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private Vector3 _originalPosition;
    private int _originalIndex;

    [SerializeField]
    private float _returnSpeed = 500.0f;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void OnDrag(DragEventArgs args) {
        Ray ray = Camera.main.ScreenPointToRay(args.TrackedFinger.position);
        Vector3 worldPosition = ray.GetPoint(10);
        this.transform.position = worldPosition;
        this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
    }

    public void OnRelease(Vector2 releasePoint) {
        GameObject hitObject = null;
        Ray ray = Camera.main.ScreenPointToRay(releasePoint);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Purchase Collider"))) {
            hitObject = hit.collider.gameObject;
        }

        if(hitObject != null) {
            bool result = PlayerManager.Instance.BuyRandomMinion();
            if(result) {
                CardContainer cardContainer = this.GetComponentInParent<CardContainer>();
                cardContainer.ConsumeCard(this);
                AudioManager.Instance.Play(EAudioId.BUY_RANDOM_MINION);
            }
            else {
                AudioManager.Instance.Play(EAudioId.NOT_ENOUGH_COINS);
                AudioManager.Instance.Play(EAudioId.CARD_RELEASE);
            }
        }
        else
            AudioManager.Instance.Play(EAudioId.CARD_RELEASE);
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        LIFECYCLE METHODS                        |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Start() {
        this._originalPosition = this.transform.position;
        this._originalIndex = this.transform.GetSiblingIndex();
    }

    private void Update() {
        if(this.transform.position != this._originalPosition) {
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                          this._originalPosition,
                                                          this._returnSpeed * Time.deltaTime);
        }
        else
            this.transform.SetSiblingIndex(this._originalIndex);
    }
}
