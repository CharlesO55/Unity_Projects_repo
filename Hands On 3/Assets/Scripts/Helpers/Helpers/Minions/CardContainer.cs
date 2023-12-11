using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 |                  You may NOT edit this script.                  |
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class CardContainer : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    [SerializeField]
    private GameObject _cardPointReferences;
    private List<Vector3> _cardSpawnLocations = new List<Vector3>();

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public IEnumerator Show() {
        foreach(Scaler child in this.GetComponentsInChildren<Scaler>(true)) {
            this.Activate(child.gameObject);
            yield return child.ScalePulse();
        }
    }

    public IEnumerator Hide() {
        Scaler[] children = this.GetComponentsInChildren<Scaler>(true);
        System.Array.Reverse(children);

        foreach(Scaler child in children) {
            yield return child.ScalePulse();
            this.Deactivate(child.gameObject);
        }
    }

    public void ConsumeCard(CardMinionManager cardMinion) {
        cardMinion.GetComponentInChildren<MeshRenderer>().enabled = false;
        cardMinion.GetComponent<MeshCollider>().enabled = false;
    }

    private void Activate(GameObject gameObject) {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.transform.parent.GetComponent<MeshCollider>().enabled = true;
    }

    private void Deactivate(GameObject gameObject) {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.parent.GetComponent<MeshCollider>().enabled = false;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        LIFECYCLE METHODS                        |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Start() {
        if(this._cardPointReferences != null) {
            foreach(Transform child in this._cardPointReferences.transform) {
                this._cardSpawnLocations.Add(child.position);
            }
        }
    }
}
