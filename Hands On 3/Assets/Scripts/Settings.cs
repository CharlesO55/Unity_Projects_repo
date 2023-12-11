using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                  You may NOT edit this script.                  |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class Settings : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static Settings Instance;

    [SerializeField]
    private LayerMask _gestureIgnoredLayers;
    public LayerMask GestureIgnoredLayers {
        get { return this._gestureIgnoredLayers; }
    }

    [SerializeField]
    private LayerMask _playerMinionHitLayers;
    public LayerMask PlayerMinionHitLayers {
        get { return this._playerMinionHitLayers; }
    }

    [SerializeField]
    private int _maxMinionCount = 6;
    public int MaxMinionCount {
        get { return _maxMinionCount; }
    }

    [SerializeField]
    private int _cardMinionCost = 3;
    public int CardMinionCost {
        get { return _cardMinionCost; }
    }

    [SerializeField]
    private int _maxTavernLevel = 6;
    public int MaxTavernLevel {
        get { return _maxTavernLevel; }
    }

    [SerializeField]
    private int _rerollTavernCost = 1;
    public int RerollTavernCost {
        get { return _rerollTavernCost; }
    }

    [SerializeField]
    private int _coinLimit = 10;
    public int CoinLimit {
        get { return _coinLimit; }
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
    }
}
