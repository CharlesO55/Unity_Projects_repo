using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour, ISwappable {
    [SerializeField]
    private Scaler _endTurn;

    [SerializeField]
    private Scaler _startTurn;

    public IEnumerator SwapTavern() {
        this.Activate(this._endTurn.gameObject);
        yield return this._endTurn.ScalePulse();
        this.Deactivate(this._endTurn.gameObject);

        this.Activate(this._startTurn.gameObject);
        yield return this._startTurn.ScalePulse();
    }

    public IEnumerator SwapBattleground() {
        this.Activate(this._startTurn.gameObject);
        yield return this._startTurn.ScalePulse();
        this.Deactivate(this._startTurn.gameObject);

        this.Activate(this._endTurn.gameObject);
        yield return this._endTurn.ScalePulse();
    }

    private void Activate(GameObject gameObject) {
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.GetComponentInChildren<TMP_Text>().enabled = true;
        gameObject.transform.parent.GetComponent<MeshCollider>().enabled = true;
    }

    private void Deactivate(GameObject gameObject) {
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.GetComponentInChildren<TMP_Text>().enabled = false;
        gameObject.transform.parent.GetComponent<MeshCollider>().enabled = false;
    }
}
