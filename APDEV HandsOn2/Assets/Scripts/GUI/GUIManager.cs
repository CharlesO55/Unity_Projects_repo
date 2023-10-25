using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                         DO NOT EDIT THIS SCRIPT                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class GUIManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static GUIManager Instance;
    private VisualElement _root;
    private EEquipmentType _equipmentType;
    public EEquipmentType EquipmentType {
        get { return this._equipmentType; }
    }

    private Image _targetTop;
    private Image _targetBottom;

    private Image _emfTop;
    private Image _emfBottom;
    private List<Image> _emfTops = new List<Image>();
    private List<Image> _emfBottoms = new List<Image>();
    private Image _spiritBoxTop;
    private Image _spiritBoxBottom;

    private float _speedSlide = 1200.0f;
    private bool _isSlidingUp = false;
    private bool _isSlidingDown = false;

    private bool _isOpen = false;
    public bool IsOpen {
        get { return _isOpen; }
    }

    /* * * * * FOR DEVELOPMENT * * * * *
     * ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ */

    [SerializeField]
    private bool _openEMF = false;

    [SerializeField]
    private bool _openSpiritBox = false;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void OpenEMF() {
        if(!this._isSlidingUp && !this._isSlidingDown) {
            this.HideAllEquipment();

            this._equipmentType = EEquipmentType.EMF;
            this._targetTop = this._emfTop;
            this._targetBottom = this._emfBottom;

            this.ShowEMFLevel1();
            this.StartOpenAnimation();
            EquipmentManager.Instance.PlayClick();
        }
    }

    public void OpenSpiritBox() {
        if(!this._isSlidingUp && !this._isSlidingDown) {
            this.HideAllEquipment();

            this._equipmentType = EEquipmentType.SPIRIT_BOX;
            this._targetTop = this._spiritBoxTop;
            this._targetBottom = this._spiritBoxBottom;

            this.StartOpenAnimation();
            SpiritBoxSFXManager.Instance.TurnOn();
        }
    }
    public void Close() {
        if(!this._isSlidingUp && !this._isSlidingDown) {
            this._equipmentType = EEquipmentType.NONE;
            this.StartCloseAnimation();
            if(this._targetTop == this._spiritBoxTop)
                SpiritBoxSFXManager.Instance.TurnOff();
            else
                EMFSFXManager.Instance.TurnOff();
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                            LIFECYCLE METHODS                            *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start() {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._root.RegisterCallback<GeometryChangedEvent>(this.OnRootRender);
        this._equipmentType = EEquipmentType.NONE;

        this._targetTop = null;
        this._targetBottom = null;

        this._emfTop = this._root.Q<Image>("EMFTop");
        this._emfBottom = this._root.Q<Image>("EMFBottom");
        this._spiritBoxTop = this._root.Q<Image>("SpiritBoxTop");
        this._spiritBoxBottom = this._root.Q<Image>("SpiritBoxBottom");

        this._emfTops.Add(this._root.Q<Image>("EMFTop1"));
        this._emfTops.Add(this._root.Q<Image>("EMFTop2"));
        this._emfTops.Add(this._root.Q<Image>("EMFTop3"));
        this._emfTops.Add(this._root.Q<Image>("EMFTop4"));
        this._emfTops.Add(this._root.Q<Image>("EMFTop5"));

        this._emfBottoms.Add(this._root.Q<Image>("EMFBottom1"));
        this._emfBottoms.Add(this._root.Q<Image>("EMFBottom2"));
        this._emfBottoms.Add(this._root.Q<Image>("EMFBottom3"));
        this._emfBottoms.Add(this._root.Q<Image>("EMFBottom4"));
        this._emfBottoms.Add(this._root.Q<Image>("EMFBottom5"));

        this.HideAllEquipment();
    }

    private void Update() {
        if(this._isOpen) {
            if(this._isSlidingDown)
                this.SlideDown(this._targetTop);
            if(this._isSlidingUp)
                this.SlideUp(this._targetBottom);
        }
        else {
            if(this._isSlidingDown)
                this.SlideDown(this._targetTop);
            if(this._isSlidingUp)
                this.SlideUp(this._targetBottom);
        }

        /* * * * * FOR DEVELOPMENT * * * * *
         * ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ */

        if(this._openEMF) {
            this.OpenEMF();
            this._openEMF = false;
        }

        if(this._openSpiritBox) {
            this.OpenSpiritBox();
            this._openSpiritBox = false;
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *            HELPER METHODS THAT YOU DON'T NEED TO WORRY ABOUT            *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void OnRootRender(GeometryChangedEvent evt) {
        this._root.UnregisterCallback<GeometryChangedEvent>(this.OnRootRender);
    }

    private void HideAllEquipment() {
        this._emfTop.style.visibility = Visibility.Hidden;
        this._emfBottom.style.visibility = Visibility.Hidden;
        this._spiritBoxTop.style.visibility = Visibility.Hidden;
        this._spiritBoxBottom.style.visibility = Visibility.Hidden;
    }

    private void HideUpwards(VisualElement target) {
        float height = target.resolvedStyle.height;
        target.style.top = -height;
    }

    private void HideDownwards(VisualElement target) {
        float height = target.resolvedStyle.height;
        target.style.bottom = -height;
    }

    private void SlideDown(VisualElement target) {
        Length top = target.style.top.value;

        if(this._isOpen) {
            target.style.top = top.value + (this._speedSlide * Time.deltaTime);
            if(target.style.top.value.value >= 0) {
                target.style.top = 0;
                this._isSlidingDown = false;
            }
        }
        else {
            target.style.top = top.value - (this._speedSlide * Time.deltaTime);
            if(target.style.top.value.value <= -(target.resolvedStyle.height)) {
                target.style.top = -(target.resolvedStyle.height);
                this._isSlidingDown = false;
                this.HideAllEquipment();
            }
        }
    }

    private void SlideUp(VisualElement target) {
        Length bottom = target.style.bottom.value;

        if(this._isOpen) {
            target.style.bottom = bottom.value + (this._speedSlide * Time.deltaTime);
            if(target.style.bottom.value.value >= 0) {
                target.style.bottom = 0;
                this._isSlidingUp = false;
            }
        }
        else {
            target.style.bottom = bottom.value - (this._speedSlide * Time.deltaTime);
            if(target.style.bottom.value.value <= -(target.resolvedStyle.height)) {
                target.style.bottom = -(target.resolvedStyle.height);
                this._isSlidingUp = false;
                this.HideAllEquipment();
            }
        }
    }

    private void StartOpenAnimation() {
        this._isOpen = true;
        this._targetTop.style.visibility = Visibility.Visible;
        this._targetBottom.style.visibility = Visibility.Visible;

        this.HideUpwards(this._targetTop);
        this.HideDownwards(this._targetBottom);
        this._isSlidingUp = true;
        this._isSlidingDown = true;
    }

    private void StartCloseAnimation() {
        this._isOpen = false;
        this._isSlidingUp = true;
        this._isSlidingDown = true;
    }

    public void UpdateEquipment(EColliderLevel level) {
        if(this._isOpen) {
            if(this._targetTop == this._emfTop) {
                switch(level) {
                    case EColliderLevel.LEVEL_1:
                        this.ShowEMFLevel1();
                        EMFSFXManager.Instance.PlayLevel1();
                        break;

                    case EColliderLevel.LEVEL_2:
                        this.ShowEMFLevel2();
                        EMFSFXManager.Instance.PlayLevel2();
                        break;

                    case EColliderLevel.LEVEL_3:
                        this.ShowEMFLevel3();
                        EMFSFXManager.Instance.PlayLevel3();
                        break;

                    case EColliderLevel.LEVEL_4:
                        this.ShowEMFLevel4();
                        EMFSFXManager.Instance.PlayLevel4();
                        break;

                    case EColliderLevel.LEVEL_5:
                        this.ShowEMFLevel5();
                        EMFSFXManager.Instance.PlayLevel5();
                        break;
                }
            }
        }
    }

    private void HideAllEMFLevels() {
        foreach(Image image in this._emfTops)
            image.style.visibility = Visibility.Hidden;

        foreach(Image image in this._emfBottoms)
            image.style.visibility = Visibility.Hidden;
    }

    private void ShowEMFLevel1() {
        this.HideAllEMFLevels();
        this._emfTops[0].style.visibility = Visibility.Visible;
        this._emfBottoms[0].style.visibility = Visibility.Visible;
    }

    private void ShowEMFLevel2() {
        this.HideAllEMFLevels();
        this._emfTops[1].style.visibility = Visibility.Visible;
        this._emfBottoms[1].style.visibility = Visibility.Visible;
    }

    private void ShowEMFLevel3() {
        this.HideAllEMFLevels();
        this._emfTops[2].style.visibility = Visibility.Visible;
        this._emfBottoms[2].style.visibility = Visibility.Visible;
    }

    private void ShowEMFLevel4() {
        this.HideAllEMFLevels();
        this._emfTops[3].style.visibility = Visibility.Visible;
        this._emfBottoms[3].style.visibility = Visibility.Visible;
    }

    private void ShowEMFLevel5() {
        this.HideAllEMFLevels();
        this._emfTops[4].style.visibility = Visibility.Visible;
        this._emfBottoms[4].style.visibility = Visibility.Visible;
    }
}
