using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                  You may NOT edit this script.                  |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class BoardManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static BoardManager Instance;

    [SerializeField]
    private BackgroundManager _backgroundManager;

    [SerializeField]
    private MeshCollider _purchaseCollider;

    [SerializeField]
    private MeshCollider _sellCollider;

    /* Unity does NOT allow interfaces to appear in the Editor... */
    [SerializeField]
    private List<GameObject> _swappableObjects;
    private List<ISwappable> _swappables = new List<ISwappable>();

    private bool _ready = true;
    public bool Ready {
        get { return this._ready; }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public IEnumerator OpenBattleground() {
        yield return TavernManager.Instance.RerollCoroutine;

        if(this._ready) {
            AudioManager.Instance.Play(EAudioId.OPEN_BATTLEGROUND);
            this._ready = false;

            this._purchaseCollider.enabled = false;
            this._sellCollider.enabled = false;

            Coroutine hideLevel = this.StartCoroutine(TavernManager.Instance.HideLevel());
            Coroutine swapBattleground = this.StartCoroutine(this.SwapBattleground());
            this._backgroundManager.OpenBattleground();
            Coroutine refreshCoins = CoinManager.Instance.RefreshCoins();

            BattlegroundManager.Instance.AddRandomMinion();

            yield return this.ShowBattlegroundMinions();
            yield return hideLevel;
            yield return swapBattleground;
            yield return refreshCoins;

            while(this._backgroundManager.BoardOpen.isPlaying)
                yield return null;

            this._ready = true;
        }
    }

    public IEnumerator OpenTavern() {
        if(this._ready) {
            AudioManager.Instance.Play(EAudioId.OPEN_TAVERN);
            this._ready = false;

            this._purchaseCollider.enabled = true;
            this._sellCollider.enabled = true;

            TavernManager.Instance.DeductUpgradeCost();
            Coroutine swapTavern = this.StartCoroutine(this.SwapTavern());
            this._backgroundManager.OpenTavern();

            yield return this.ShowTavernCards();
            yield return swapTavern;

            while(this._backgroundManager.BoardClose.isPlaying)
                yield return null;

            this._ready = true;
        }
    }

    private IEnumerator SwapTavern() {
        List<Coroutine> coroutines = new List<Coroutine>();
        foreach(ISwappable swappable in this._swappables) {
            coroutines.Add(this.StartCoroutine(swappable.SwapTavern()));
        }

        foreach(Coroutine coroutine in coroutines)
            yield return coroutine;
    }

    private IEnumerator SwapBattleground() {
        List<Coroutine> coroutines = new List<Coroutine>();
        foreach(ISwappable swappable in this._swappables) {
            coroutines.Add(this.StartCoroutine(swappable.SwapBattleground()));
        }

        foreach(Coroutine coroutine in coroutines)
            yield return coroutine;
    }

    private IEnumerator ShowTavernCards() {
        yield return BattlegroundManager.Instance.HideMinions();
        yield return TavernManager.Instance.ShowCards();
        this.StartCoroutine(TavernManager.Instance.ShowLevel());
    }

    private IEnumerator ShowBattlegroundMinions() {
        yield return TavernManager.Instance.HideCards();
        yield return BattlegroundManager.Instance.ShowMinions();
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

    private void Start() {
        foreach(GameObject swappableObject in this._swappableObjects) {
            this._swappables.Add(swappableObject.GetComponent<ISwappable>());
        }

        CoinManager.Instance.RefreshCoins();
    }
}
