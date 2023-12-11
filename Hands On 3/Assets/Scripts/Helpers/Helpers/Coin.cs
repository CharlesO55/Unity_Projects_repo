using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    [SerializeField]
    private Scaler _coinCover;

    public IEnumerator ScaleUp() {
        yield return this._coinCover.ScaleUp();
    }

    public IEnumerator ScaleDown() {
        yield return this._coinCover.ScaleDown();
    }

    public IEnumerator ScalePulse() {
        yield return this._coinCover.ScalePulse();
    }

    public void Show() {
        this._coinCover.gameObject.SetActive(true);
    }

    public void Hide() {
        this._coinCover.gameObject.SetActive(false);
    }
}
