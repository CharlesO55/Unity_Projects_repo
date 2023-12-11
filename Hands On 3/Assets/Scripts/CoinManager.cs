using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 |                  You may NOT edit this script.                  |
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class CoinManager : MonoBehaviour {
    public static CoinManager Instance;

    private int _baseCoins = 2;
    private int _currentCoins;
    public int CurrentCoins {
        get { return this._currentCoins; }
    }

    private List<Coin> _coins = new List<Coin>();

    public IEnumerator ShowCoins() {
        foreach(Coin coin in this._coins) {
            coin.Hide();
        }

        for(int i = 0; i < this._currentCoins && i < this._coins.Count; i++) {
            this._coins[i].Show();
            yield return this._coins[i].ScaleUp();

            int j = i + 1;
            if(j < this._currentCoins && j < this._coins.Count) {
                this._coins[j].Show();
                this._coins[j].ScaleUp();
            }

            yield return this._coins[i].ScaleDown();
        }
    }

    /* Every time the coins are refreshed, the base coin
     * limit is also increased by [1], to a maximum of
     * [Settings.CoinLimit]. */ 
    public Coroutine RefreshCoins() {
        AudioManager.Instance.Play(EAudioId.REFRESH_COINS);

        if(this._currentCoins < Settings.Instance.CoinLimit) {
            this._baseCoins++;
            this._currentCoins = this._baseCoins;
        }

        Coroutine showCoins = this.StartCoroutine(this.ShowCoins());
        return showCoins;
    }

    public IEnumerator IncreaseCoin() {
        if(this._currentCoins < Settings.Instance.CoinLimit) {
            int index = this._currentCoins;
            this._currentCoins++;

            this._coins[index].Show();
            yield return this._coins[index].ScalePulse();
        }
    }

    private IEnumerator DeductCoin() {
        if(this._currentCoins > 0) {
            this._currentCoins--;
            int index = this._currentCoins;

            yield return this._coins[index].ScalePulse();
            this._coins[index].Hide();
        }
    }

    public IEnumerator DeductCoins(int value) {
        if(this._currentCoins >= value) {
            for(int i = 0; i < value; i++) {
                yield return this.DeductCoin();
            }
        }
    }

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start() {
        this.GetComponentsInChildren(true, this._coins);
        this._coins.Reverse();
    }
}
