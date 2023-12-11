using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                  You may NOT edit this script.                  |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class TavernManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static TavernManager Instance;

    private CardContainer _cards;
    private TavernLevel _tavernLevel;
    public int Level {
        get { return this._tavernLevel.Level; }
    }

    private int _rerollCost;
    public int RerollCost {
        get { return this._rerollCost; }
    }

    [SerializeField]
    private int _upgradeCost;
    public int UpgradeCost {
        get { return this._upgradeCost; }
    }

    private List<int> _upgradeCosts = new List<int> {5, 7, 8, 9, 10};
    private Coroutine _rerollCoroutine = null;
    public Coroutine RerollCoroutine {
        get { return this._rerollCoroutine; }
    }

    [SerializeField]
    private CoinCost _upgradeTavernCost;

    [SerializeField]
    private CoinCost _rerollTavernCost;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public IEnumerator ShowCards() {
        yield return this.StartCoroutine(this._cards.Show());
    }

    public IEnumerator HideCards() {
        yield return this._cards.Hide();
    }

    public IEnumerator ShowLevel() {
        this._tavernLevel.gameObject.GetComponent<Image>().enabled = true;
        yield return this._tavernLevel.ScalePulse();
    }

    public IEnumerator HideLevel() {
        yield return this._tavernLevel.ScalePulse();
        this._tavernLevel.gameObject.GetComponent<Image>().enabled = false;
    }

    public void Upgrade() {
        if(this._tavernLevel.Level < (Settings.Instance.MaxTavernLevel - 1)) {
            this.SetUpgradeCost(this._upgradeCosts[this._tavernLevel.Level]);
            this.StartCoroutine(this._tavernLevel.LevelUp());
        }
    }

    public void Reroll() {
        this._rerollCoroutine = this.StartCoroutine(this.ShowCards());
    }

    public void DeductUpgradeCost() {
        if(this._upgradeCost > 0)
            this.SetUpgradeCost(this._upgradeCost - 1);
    }

    private void SetUpgradeCost(int cost) {
        this._upgradeCost = cost;
        this._upgradeTavernCost.SetCost(this._upgradeCost);
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

        this._tavernLevel = this.GetComponentInChildren<TavernLevel>();
    }

    private void Start() {
        this._cards = this.GetComponentInChildren<CardContainer>(true);
        this._tavernLevel = this.GetComponentInChildren<TavernLevel>(true);

        this._rerollCost = Settings.Instance.RerollTavernCost;
        this._rerollTavernCost.SetCost(this._rerollCost);

        this.SetUpgradeCost(this._upgradeCosts[0]);
    }
}
