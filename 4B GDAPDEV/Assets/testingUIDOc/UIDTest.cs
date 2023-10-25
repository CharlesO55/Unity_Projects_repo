using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIDTest : MonoBehaviour
{
    private UIDocument _UIDoc;
    private Button _testerBtn;

    private Image _testImage;

    private void OnEnable()
    {
        _UIDoc = GetComponent<UIDocument>();

        _testerBtn = _UIDoc.rootVisualElement.Q<Button>("TestButton");
        _testerBtn.clicked += () => { Debug.Log("Clicked ver. " + _testerBtn.name); };


        _testImage = _UIDoc.rootVisualElement.Q<Image>("TestImage");

        Debug.Log("instance " + _testImage);
        _testImage.RegisterCallback<ClickEvent> (TestFunc); 
    }


    private void TestFunc(ClickEvent e)
    {
        Debug.Log("CAAAAAAAAAAAAAAAAAAAAAAAAAAAl");
        Debug.Log("Register callback ver " + e.target);
    }
}
