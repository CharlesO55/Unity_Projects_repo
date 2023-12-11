using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernButtons : MonoBehaviour, ISwappable {
    [SerializeField]
    private Transform _exitPoint;
    private Vector3 _enterPosition;
    private float _speed = 5.0f;

    public IEnumerator SwapTavern() {
        while(this.transform.position != this._enterPosition) {
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                          this._enterPosition,
                                                          this._speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator SwapBattleground() {
        while(this.transform.position != this._exitPoint.position) {
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                          this._exitPoint.position,
                                                          this._speed * Time.deltaTime);
            yield return null;
        }
    }

    private void Start() {
        this._enterPosition = this.gameObject.transform.position;
    }
}
