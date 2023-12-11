using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Portrait : MonoBehaviour, ISwappable {
    [SerializeField]
    private Scaler _enemyPortrait;

    [SerializeField]
    private Scaler _bartenderPortrait;

    [SerializeField]
    private GameObject _tavernButtons;

    [SerializeField]
    private bool _openTavern = false;

    [SerializeField]
    private bool _openBattleground = false;

    public IEnumerator SwapTavern() {
        this._enemyPortrait.gameObject.SetActive(true);
        yield return this._enemyPortrait.ScalePulse();
        this._enemyPortrait.gameObject.SetActive(false);

        this._bartenderPortrait.gameObject.SetActive(true);
        this.StartCoroutine(this._tavernButtons.GetComponent<ISwappable>().SwapTavern());
        yield return this._bartenderPortrait.ScalePulse();
    }

    public IEnumerator SwapBattleground() {
        this.StartCoroutine(this._tavernButtons.GetComponent<ISwappable>().SwapBattleground());
        this._bartenderPortrait.gameObject.SetActive(true);
        yield return this._bartenderPortrait.ScalePulse();
        this._enemyPortrait.gameObject.SetActive(false);

        this._enemyPortrait.gameObject.SetActive(true);
        yield return this._enemyPortrait.ScalePulse();
    }

    private void Update() {
        if(this._openTavern) {
            this._openTavern = false;
            this.StartCoroutine(this.SwapTavern());
        }

        if(this._openBattleground) {
            this._openBattleground = false;
            this.StartCoroutine(this.SwapBattleground());
        }
    }
}
