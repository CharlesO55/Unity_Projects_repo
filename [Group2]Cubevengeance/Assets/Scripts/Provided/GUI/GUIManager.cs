using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GUIManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                               PROPERTIES                                *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static GUIManager Instance;
    private VisualElement _root;
    private Label _score;
    private Label _speed;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void SetSpeed(float speed) {
        this._speed.text = speed + "%";
    }
    public void SetScore(int score) {
        this._score.text = score.ToString("D4");
    }

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                            LIFECYCLE METHODS                            *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Start() {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._score = this._root.Q<Label>("ScoreValue");
        this._speed = this._root.Q<Label>("SpeedValue");
    }
}
