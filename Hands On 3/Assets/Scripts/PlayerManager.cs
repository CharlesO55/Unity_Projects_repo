using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                    This script has [TODO]'s.                    |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class PlayerManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static PlayerManager Instance;
    private MinionContainer _minions;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        INCOMPLETE METHODS                       |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    /* [TODO] : This is the method that adds one minion to the PLAYER'S
     *          side of the board in the Battleground. It is called once
     *          the Player (attempts) to purcheses a minion by dragging
     *          a Card towards the Player's portrait in the TAVERN.
     *          
     *          Complete the code such that a RANDOM [MinionData] is
     *          chosen from the [WebAPIManager]'s list of [MinionData]
     *          (i.e. from the [MinionTiers] field), whenever the Player
     *          attempts to purchases a minion
     *          
     *          The tier (or cost) of the chosen minion must only be
     *          between [1] and the [CURRENT TAVERN LEVEL], inclusive.
     *          
     *          The cost of purchasing ONE minion is [3] coins. Do NOT
     *          allow the Player to proceed with the purchase if their
     *          current number of coins is NOT enough.
     *          
     *          The maximum number of minions in the Player's side of
     *          the Battleground is [6]. Do NOT allow the Player to
     *          proceed with the purchase if their side of the board
     *          is full.
     *          
     *          This method returns [true] if the Player was able to
     *          successfully purchase a minion, and [false] otherwise.
     * 
     * [REFERENCES] :
     * 
     *    [1] [TavernManager.Instance.Level] returns the Player's current
     *        tavern level.
     *        
     *    [2] [CoinManager.Instance.CurrentCoins] returns the Player's
     *        current number of coins.
     *        
     *    [3] [Settings.Instance.CardMinionCost] returns the cost of buying
     *        ONE random minion.
     *        
     *    [4] [this._minions.IsFull()] returns true if the number of minions
     *        on the Player's side of the Battleground is already full. */

    public bool BuyRandomMinion() {

        /* [1] : Replace the condition inside the [if()] statement with the
                 correct one, following the outlined rules, if any. */
        if(!this._minions.IsFull() && 
            //INSTRUCTIONS STATED THAT COST IS 3
            (CoinManager.Instance.CurrentCoins - 3 >= 0)) {

            /* [2] : Store in the [data] variable the RANDOMLY selected
                     [MinionData], following the outlined rules, if any. */
            MinionData data = null;

            /* * * * * [YOUR CODE HERE] * * * * */
            int nRandomTier = Random.Range(1, TavernManager.Instance.Level +1);
            List<MinionData> lotsA = WebAPIManager.Instance.MinionTiers[nRandomTier];

            int nRandomIndex = Random.Range(0, lotsA.Count);
            data = lotsA[nRandomIndex];

            //NO MENTION OF UPDATING COINS

            /* Provided code, do NOT remove. */
            this._minions.Add(data);
            this.StartCoroutine(CoinManager.Instance.DeductCoins(Settings.Instance.CardMinionCost));

            return true;
        }
        return false;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void SellMinion(MinionManager minion) {
        AudioManager.Instance.Play(EAudioId.SELL_MINION);
        this.StartCoroutine(CoinManager.Instance.IncreaseCoin());
        this.StartCoroutine(this.KillMinion(minion));
    }

    public IEnumerator KillMinion(MinionManager minion) {
        yield return minion.ScalePulse();
        yield return this.StartCoroutine(this._minions.Remove(minion));
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
