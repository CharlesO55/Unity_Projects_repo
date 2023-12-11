using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TavernLevel : MonoBehaviour {
    [SerializeField]
    private List<Sprite> _sprites;

    private int _level = 1;
    public int Level {
        get { return this._level; }
    }

    private Scaler _scaler;
    private Image _image;

    [SerializeField]
    private bool _levelUp = false;

    public IEnumerator LevelUp() {
        if(this._level < this._sprites.Count) {
            this._level++;
            yield return this.ScaleUp();
            this._image.sprite = this._sprites[this._level - 1];
            yield return this.ScaleDown();
        }
    }

    public IEnumerator ScaleUp() {
        yield return this.StartCoroutine(this._scaler.ScaleUp());
    }

    public IEnumerator ScaleDown() {
        yield return this.StartCoroutine(this._scaler.ScaleDown());
    }

    public IEnumerator ScalePulse() {
        yield return this.StartCoroutine(this._scaler.ScalePulse());
    }

    private void Start() {
        this._scaler = this.GetComponent<Scaler>();
        this._image = this.GetComponent<Image>();
        this._image.sprite = this._sprites[0];
    }

    private void Update() {
        if(this._levelUp) {
            this._levelUp = false;
            this.StartCoroutine(this.LevelUp());
        }
    }
}
