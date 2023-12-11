using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                  You may NOT edit this script.                  |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class MinionContainer : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    [SerializeField]
    private GameObject _minionPointReferences;

    private GameObject _minionReference;
    private List<Vector3> _minionSpawnLocations = new List<Vector3>();
    private List<GameObject> _minions = new List<GameObject>();

    [SerializeField]
    private bool _add = false;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public MinionManager Add(MinionData data, bool showImmediately = true) {
        if(!this.IsFull()) {
            GameObject minion = Instantiate(this._minionReference);

            int index = this._minions.Count;
            minion.transform.parent = this.transform;
            minion.transform.position = this._minionSpawnLocations[index];
            this._minions.Add(minion);

            MinionManager handler = minion.GetComponent<MinionManager>();
            if(handler != null) {
                handler.RefreshSiblingIndex();
                handler.SetData(data);

                if(showImmediately)
                    this.StartCoroutine(handler.ScalePulse());
            }
            return handler;
        }
        return null;
    }

    public IEnumerator Remove(MinionManager minion) {
        this._minions[minion.transform.GetSiblingIndex()] = null;
        Destroy(minion.gameObject);
        while(minion != null)
            yield return null;
        this.RefreshMinions();
    }

    public IEnumerator Show() {
        foreach(MinionManager child in this.GetComponentsInChildren<MinionManager>(true)) {
            AudioManager.Instance.Play(EAudioId.OPEN_BATTLEGROUND);
            child.Activate();
            yield return child.ScalePulse();
        }
    }

    public IEnumerator Hide() {
        MinionManager[] children = this.GetComponentsInChildren<MinionManager>(true);
        System.Array.Reverse(children);

        foreach(MinionManager child in children) {
            yield return child.ScalePulse();
            child.Deactivate();
        }
    }

    private void RefreshMinions() {
        List<GameObject> minions = new List<GameObject>();

        int index = 0;
        foreach(MinionManager child in this.GetComponentsInChildren<MinionManager>(true)) {
            if(child != null) {
                child.SetPosition(this._minionSpawnLocations[index]);
                child.gameObject.transform.position = this._minionSpawnLocations[index];
                minions.Add(child.gameObject);
                index++;
            }
        }

        this._minions = minions;
    }

    public MinionManager GetDeadMinion() {
        MinionManager minionManager = null;
        bool found = false;

        for(int i = 0; i < this._minions.Count && !found; i++) {
            GameObject minion = this._minions[i];
            if(minion != null) {
                minionManager = minion.GetComponent<MinionManager>();
                if(minionManager.HealthValue == 0)
                    found = true;
            }
        }

        if(found)
            return minionManager;
        else
            return null;
    }

    public bool IsFull() {
        return (this._minions.Count >= Settings.Instance.MaxMinionCount);
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        LIFECYCLE METHODS                        |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Start() {
        this._minionReference = PrefabManager.Instance.Minion;

        if(this._minionPointReferences != null) {
            foreach(Transform child in this._minionPointReferences.transform) {
                this._minionSpawnLocations.Add(child.position);
            }
        }

        this.RefreshMinions();
    }
}
