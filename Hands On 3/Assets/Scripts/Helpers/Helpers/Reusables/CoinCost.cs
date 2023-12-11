using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCost : MonoBehaviour {
    private TMP_Text _cost;

    public void SetCost(int cost) {
        this._cost.SetText(cost.ToString());
    }

    private void Awake() {
        this._cost = GetComponentInChildren<TMP_Text>();
    }
}
