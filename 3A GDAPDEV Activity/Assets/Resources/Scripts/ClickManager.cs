using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClickManager : MonoBehaviour
{
    private UIDocument HUD;
    private Button DrawButton;

    private VisualElement CardsContainer;
    private Image CardSpade;
    private Image CardDiamond;
    private Image CardHeart;
    void Start()
    {
        this.HUD = this.GetComponent<UIDocument>();
        this.DrawButton = HUD.rootVisualElement.Q("DrawButton") as Button;
        if(DrawButton != null)
        {
            Debug.Log("[FOUND]: " + DrawButton.name);
            DrawButton.RegisterCallback<ClickEvent>(DrawCard);
        }

        this.CardsContainer = HUD.rootVisualElement.Q("Cards");

        this.CardSpade = HUD.rootVisualElement.Q("CardSpade") as Image;
        CardSpade.RegisterCallback<ClickEvent>(SelectCard);

        this.CardDiamond= HUD.rootVisualElement.Q("CardDiamond") as Image;
        CardDiamond.RegisterCallback<ClickEvent>(SelectCard);

        this.CardHeart = HUD.rootVisualElement.Q("CardHeart") as Image;
        CardHeart.RegisterCallback<ClickEvent>(SelectCard);
    }

    public void SelectCard(ClickEvent evt)
    {
        Image Card = evt.target as Image;

        Debug.Log("Selected [Target]: " + evt.target /*+ " [Current Target]: " + evt.currentTarget*/);
        Card.visible = false;
        this.CardsContainer.Remove(Card);
    }


    public void DrawCard(ClickEvent evt)
    {
        Image[] Cards = { this.CardSpade, this.CardDiamond, this.CardHeart };
        
        foreach (Image card in Cards){
            if (card.visible == false)
            {
                this.CardsContainer.Add(card);
                card.visible = true;
                Debug.Log("[DRAWN]: " + card.name);
                return;
            }
        }
        Debug.Log("[DRAWN]: NO CARDS AVAILABLE");
    }
}