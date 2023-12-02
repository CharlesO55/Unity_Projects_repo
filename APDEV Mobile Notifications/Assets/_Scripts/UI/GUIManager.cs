using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;

    [SerializeField] private UIDocument _doc;
    private VisualElement _root;

    public Button BtnSendNotif;
    public Button BtnSendRepeating;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            this._root = _doc.rootVisualElement;
            this.BtnSendNotif = _root.Q<Button>("SendNotifications");
            this.BtnSendRepeating = _root.Q<Button>("SendRepeatingNotification");

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
