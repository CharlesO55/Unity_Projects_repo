using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonOrientationLock : MonoBehaviour
{
    private Button _orientButton;


    private void OnEnable()
    {
        this._orientButton =  this.GetComponent<UIDocument>().rootVisualElement.Q<Button>("ToggleOrientation");

        this._orientButton.RegisterCallback<ClickEvent>(ToggleOrientation);
    }

    private void ToggleOrientation(ClickEvent e)
    {
        if(OrientationLock.Instance.IsLocked)
        {
            OrientationLock.Instance.Unlock();
        }
        else
        {
            OrientationLock.Instance.Lock();
        }

    }
}
