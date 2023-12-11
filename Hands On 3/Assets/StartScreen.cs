using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StartScreen : MonoBehaviour
{
    public static StartScreen Instance;
    //public bool IsButtonClicked = false;

    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        Button btn = this.GetComponent<UIDocument>().rootVisualElement.Q<Button>();
        btn.clicked += WebAPIManager.Instance.StartInitializeOnClick;
    }

    
    public void Unlink()
    {
        Button btn = this.GetComponent<UIDocument>().rootVisualElement.Q<Button>();
        btn.clicked -= WebAPIManager.Instance.StartInitializeOnClick;
    }

    public void HideScreen()
    {
        this.gameObject.SetActive(false);
    }
}
