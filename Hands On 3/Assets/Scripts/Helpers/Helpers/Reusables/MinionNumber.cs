using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinionNumber : MonoBehaviour {
    private int _numberValue;
    public int NumberValue {
        get { return this._numberValue; }
    }

    private TMP_Text _number;

    public void Activate() {
        this.GetComponent<Image>().enabled = true;
        this._number.enabled = true;
    }

    public void Deactivate() {
        this.GetComponent<Image>().enabled = false;
        this._number.enabled = false;
    }

    public void Deduct(int deduction) {
        this.SetNumber(this._numberValue - deduction);
    }

    public void SetNumber(int number) {
        this._numberValue = number;
        if(this._numberValue < 0)
            this._numberValue = 0;

        this._number.SetText(this._numberValue.ToString());
    }

    public void SetColor(Color color) {
        this._number.color = color;
    }

    private void Awake() {
        this._numberValue = 1;
        this._number = GetComponentInChildren<TMP_Text>();
    }
}
