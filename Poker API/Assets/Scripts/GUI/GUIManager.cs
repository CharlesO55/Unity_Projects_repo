using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/* * * * * * * * * * * * * * * * * * * * * *
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 |      You may NOT edit this script.      |
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 * * * * * * * * * * * * * * * * * * * * * */

public class GUIManager : MonoBehaviour {
    /* This class is a Singleton. */
    public static GUIManager Instance;

    /* Holds a reference to the [UIDocument] root. */
    private VisualElement _root;

    /* Holds a reference to the "HandRanking" [Label]. */
    private Label _handRanking;

    /* Public field for [this._handRanking]. */
    public Label HandRanking {
        get { return this._handRanking; }
    }

    /* Holds a reference to the "Draw" [Button]. */
    private Button _draw;

    /* Public field for [this._draw]. */
    public Button Draw {
        get { return this._draw; }
    }

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._handRanking = this._root.Q<Label>("HandRanking");
        this._draw = this._root.Q<Button>("Draw");
    }
}
