using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Scaler : MonoBehaviour {
    [SerializeField]
    private float _scalePercentage = 0.1f;

    [SerializeField]
    private float _speed = 0.5f;

    private Vector3 _minScale;
    private Vector3 _maxScale;

    public IEnumerator ScaleUp() {
        while(this.gameObject.transform.localScale != this._maxScale) {
            Vector3 scale = this.gameObject.transform.localScale;
            this.gameObject.transform.localScale = Vector3.MoveTowards(scale, this._maxScale, this._speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator ScaleDown() {
        while(this.gameObject.transform.localScale != this._minScale) {
            Vector3 scale = this.gameObject.transform.localScale;
            this.gameObject.transform.localScale = Vector3.MoveTowards(scale, this._minScale, this._speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator ScalePulse() {
        yield return ScaleUp();
        yield return ScaleDown();
    }

    private void Start() {
        this._minScale = this.gameObject.transform.localScale;
        this._maxScale = this._minScale + (this._minScale * this._scalePercentage);
    }
}
