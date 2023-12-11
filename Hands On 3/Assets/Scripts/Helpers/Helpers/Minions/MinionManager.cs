using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                    This script has [TODO]'s.                    |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class MinionManager : MonoBehaviour, IDraggable {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    [SerializeField]
    private Image _minionImage;

    [SerializeField]
    private Image _minionFrame;

    [SerializeField]
    private MinionNumber _minionAttack;
    public int AttackValue {
        get { return this._minionAttack.NumberValue; }
    }

    [SerializeField]
    private MinionNumber _minionHealth;
    public int HealthValue {
        get { return this._minionHealth.NumberValue; }
    }

    [SerializeField]
    private List<Scaler> _scalers = new List<Scaler>();

    private MinionData _data;

    private Vector3 _originalPosition;
    private int _originalIndex;
    private bool _hitMinion = false;
    public bool HitMinion {
        get { return this._hitMinion; }
    }

    [SerializeField]
    private bool _draggable = true;
    public bool Draggable {
        get { return this._draggable; }
        set {  this._draggable = value; }
    }

    [SerializeField]
    private float _returnSpeed = 500.0f;

    [SerializeField]
    private GameObject _frameOutline;
    public GameObject FrameOutline {
        get { return _frameOutline; }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        INCOMPLETE METHODS                       |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    /* [TODO] : This is the method that sets [this.data] with the [data]
     *          parameter.
     *          
     *          ON TOP OF THIS, you must also set the numbers of the
     *          [this._minionAttack] and [this._minionHealth] fields
     *          accordingly.
     *          
     *          This is also the method where the minion's image, stored
     *          in [this._minionImage], is set accordingly.
     *          
     * [REFERENCES] :
     * 
     *    [1] [this._minionAttack.SetNumber()] sets the text in the minion
     *        object's attack section.
     *        
     *    [2] [this._minionHealth.SetNumber()] sets the text in the minion
     *        object's health section.
     *        
     *    [3] [WebAPIManager.Instance.RequestTexture()] sets an [Image]'s
     *        sprite to the downloaded URL. You must COMPLETE this method
     *        first. */

    public void SetData(MinionData data) {
        /* * * * * [YOUR CODE HERE] * * * * */
        this._data = data;

        this._minionHealth.SetNumber(data.Health);
        this._minionAttack.SetNumber(data.Attack);
        WebAPIManager.Instance.RequestTexture(data.CardID ,this._minionImage);
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void OnDrag(DragEventArgs args) {
        if(this._draggable) {
            AudioManager.Instance.Play(EAudioId.SUMMON_MINION);
            this._frameOutline.SetActive(true);

            Ray ray = Camera.main.ScreenPointToRay(args.TrackedFinger.position);
            Vector3 worldPosition = ray.GetPoint(10);
            this.transform.position = worldPosition;

            this.transform.SetSiblingIndex(this.transform.parent.childCount);
        }
    }

    public void OnRelease(Vector2 releasePoint) {
        this._hitMinion = false;

        if(this._draggable) {
            this.FrameOutline.SetActive(false);

            GameObject hitObject = null;
            Ray ray = Camera.main.ScreenPointToRay(releasePoint);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, Settings.Instance.PlayerMinionHitLayers)) {
                hitObject = hit.collider.gameObject;
            }

            if(hitObject != null) {
                MinionManager handler = hitObject.GetComponent<MinionManager>();

                if(handler != null) {
                    this._hitMinion = true;
                    this.Damage(handler);
                }
                else {
                    PlayerManager.Instance.SellMinion(this);
                }
            }
            this.transform.SetSiblingIndex(this._originalIndex);
        }
    }

    public void Damage(MinionManager targetMinion) {
        targetMinion.DeductHealth(this.AttackValue);
        this.DeductHealth(targetMinion.AttackValue);
    }

    public void DeductHealth(int deduction) {
        this._minionHealth.Deduct(deduction);
        if(this.HealthValue < this._data.Health)
            this._minionHealth.SetColor(new Color(255, 0, 0));
        else
            this._minionHealth.SetColor(new Color(255, 255, 255));
    }

    public IEnumerator ScaleUp() {
        foreach(Scaler scaler in this._scalers) {
            this.StartCoroutine(scaler.ScaleUp());
        }
        yield return null;
    }

    public IEnumerator ScaleDown() {
        foreach(Scaler scaler in this._scalers) {
            this.StartCoroutine(scaler.ScaleDown());
        }
        yield return null;
    }

    public IEnumerator ScalePulse() {
        List<Coroutine> coroutines = new List<Coroutine>();
        foreach(Scaler scaler in this._scalers) {
            coroutines.Add(this.StartCoroutine(scaler.ScalePulse()));
        }

        foreach(Coroutine coroutine in coroutines)
            yield return coroutine;
    }

    public void Activate() {
        this.GetComponent<MeshCollider>().enabled = true;
        this._minionImage.enabled = true;
        this._minionFrame.enabled = true;
        this._minionAttack.Activate();
        this._minionHealth.Activate();
    }

    public void Deactivate() {
        this.GetComponent<MeshCollider>().enabled = false;
        this._minionImage.enabled = false;
        this._minionFrame.enabled = false;
        this._minionAttack.Deactivate();
        this._minionHealth.Deactivate();
    }

    public void RefreshSiblingIndex() {
        this._originalIndex = this.transform.GetSiblingIndex();
    }

    public void SetPosition(Vector3 position) {
        this.transform.position = position;
        this._originalPosition = this.transform.position;
        this._originalIndex = this.transform.GetSiblingIndex();
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        LIFECYCLE METHODS                        |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Start() {
        this._originalPosition = this.transform.position;
        this._originalIndex = this.transform.GetSiblingIndex();
    }

    private void Update() {
        if(this.transform.position != this._originalPosition) {
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                          this._originalPosition,
                                                          this._returnSpeed * Time.deltaTime);
        }
    }
}
