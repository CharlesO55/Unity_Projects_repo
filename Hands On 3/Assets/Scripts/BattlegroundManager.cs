using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                    This script has [TODO]'s.                    |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class BattlegroundManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static BattlegroundManager Instance;
    private MinionContainer _minions;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        INCOMPLETE METHODS                       |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    /* [TODO] : This is the method that adds one minion to the ENEMY'S
     *          side of the board in the BATTLEGROUND.
     *          
     *          Complete the code such that a RANDOM [MinionData] is
     *          chosen from the [WebAPIManager]'s list of [MinionData]
     *          (i.e. from the [MinionTiers] field).
     *          
     *          The tier (or cost) of the chosen minion must only be
     *          between [1] and the [MAX TAVERN LEVEL], inclusive.
     *          
     * [REFERENCES] :
     * 
     *    [1] [Settings.Instance. MaxTavernLevel] returns the maximum
     *        tavern level. */

    public void AddRandomMinion() {
        /* [1] : Store in the [data] variable the RANDOMLY selected
           [MinionData], following the outlined rules, if any. */
        
        MinionData data = null;

        int nRandomTier = Random.Range(1, Settings.Instance.MaxTavernLevel + 1);
        List<MinionData> lotsA = WebAPIManager.Instance.MinionTiers[nRandomTier];

        int nRandomIndex = Random.Range(0, lotsA.Count);
        data = lotsA[nRandomIndex];


        /* * * * * [YOUR CODE HERE] * * * * */

        /* Provided code, do NOT remove. */
        MinionManager minion = this._minions.Add(data, false);

        if(minion != null) {
            minion.Draggable = false;
            minion.gameObject.layer = LayerMask.NameToLayer("Enemy Collider");
            minion.Deactivate();
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    
    public IEnumerator KillMinion(MinionManager minion) {
        yield return minion.ScalePulse();
        this.StartCoroutine(this._minions.Remove(minion));
    }


    public IEnumerator ShowMinions() {
        yield return this.StartCoroutine(this._minions.Show());
    }

    public IEnumerator HideMinions() {
        yield return this.StartCoroutine(this._minions.Hide());
    }

    public bool RemoveDeadMinion() {
        MinionManager minion = this._minions.GetDeadMinion();
        if(minion != null) {
            this.StartCoroutine(this.KillMinion(minion));
            return true;
        }
        return false;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        LIFECYCLE METHODS                        |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start() {
        this._minions = this.GetComponentInChildren<MinionContainer>(true);
    }
}
