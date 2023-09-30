using UnityEngine;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{
    private VisualElement Root;
    private Button StartButton;

    private void OnEnable()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        StartButton = Root.Q<Button>("StartButton");

        StartButton.clicked += this.CallbackFunction;
    }

    public void CallbackFunction()
    {
        Debug.Log("Clicked");
    }

    void Update()
    {
        
    }
}