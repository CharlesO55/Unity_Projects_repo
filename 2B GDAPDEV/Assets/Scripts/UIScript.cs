using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{
    UIDocument UIPage;
    Button Button1;
    Button Button2;
    Button[] Buttons;

    Button HUDRoundButton;
    private void OnEnable()
    {
        UIPage = GetComponent<UIDocument>();
/*
        Button1 = UIPage.rootVisualElement.Q("Button1") as Button;
        if(Button1 != null) { 
            Debug.Log("Button1 FOund");
            Button1.RegisterCallback<ClickEvent>(myButton1Func);
        }
        Button2 = UIPage.rootVisualElement.Q("Button2") as Button;
        if(Button2 != null)
        {
            Button2.RegisterCallback<ClickEvent>(Button2Func);
        }

        HUDRoundButton = UIPage.rootVisualElement.Q("HUDRoundButton ") as Button;
        if (HUDRoundButton != null)
        {
            Debug.Log("Round Button Found");
            HUDRoundButton.RegisterCallback<ClickEvent>(myButton1Func);
        }*/

        //Buttons = UIPage.rootVisualElement.Query<Button>() as Button;

        foreach (Button button in Buttons) {
            button.RegisterCallback<ClickEvent>(myButton1Func);
        }
    }
    void myButton1Func(ClickEvent evt)
    {
        Debug.Log("Clicked");
    }

    void Button2Func(ClickEvent evt)
    {
        Debug.Log("Hide UI");
        UIPage.enabled = false;
    }
}
