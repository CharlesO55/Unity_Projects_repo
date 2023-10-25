using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                         DO NOT EDIT THIS SCRIPT                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class GhostManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static GhostManager Instance;
    private GameObject _ghost;
    private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField]
    private List<GameObject> _spawnPointParents;

    [SerializeField]
    private List<GameObject> ghosts;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Spawn(int ghostIndex, int spawnIndex) {
        this._ghost = Instantiate(this.ghosts[ghostIndex], this.spawnPoints[spawnIndex].position, this.spawnPoints[spawnIndex].rotation);
    }

    private void CreateGhost() {
        int ghostIndex = Random.Range(0, this.ghosts.Count);
        int spawnPoint = Random.Range(0, this.spawnPoints.Count);

        this.Spawn(ghostIndex, spawnPoint);
    }

    public Ghost GetGhost() {
        if(this._ghost != null)
            return this._ghost.GetComponent<Ghost>();
        return null;
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
        foreach(GameObject spawnPointParent in this._spawnPointParents)
            foreach(Transform spawnPoint in spawnPointParent.transform)
                this.spawnPoints.Add(spawnPoint);

        this.CreateGhost();
    }

    private void Update() {
        if(this._ghost == null)
            this.CreateGhost();
    }
}
