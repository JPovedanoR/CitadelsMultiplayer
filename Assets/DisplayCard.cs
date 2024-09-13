using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DisplayCard : MonoBehaviour
{
    public List<Card> displayCard = new List<Card>();
    public int displayId;

    public int id;
    public string cardName;
    public int cost;
    public int type;
    public string cardDescription;

    public Text nameText;
    public Text costText;
    public Text typeText;
    public Text descriptionText;
    public Image imageCard;


    public bool cardBack;
    public static bool staticCardBack;

    public GameObject Hand;
    public static int numberOfCardsInDeck;
    private Sprite spriteCard;
    void Start()
    {     
        numberOfCardsInDeck=PlayerDeck.deckSize;

        displayCard.Add(CardDatabase.cardList[displayId]);
        
        staticCardBack=cardBack;
        
    }

    // Update is called once per frame
    void Update()
    {

        id=displayCard[0].id;
        cardName=displayCard[0].cardName;
        cost=displayCard[0].cost;
        type=displayCard[0].type;
        cardDescription=displayCard[0].cardDescription;
        //Debug.Log(String.Format("{0}aaaa", displayCard[0].cardName));

        nameText.text=" "+cardName;
        costText.text=" "+cost;
        typeText.text=" "+type;
        descriptionText.text=" "+cardDescription;   

        spriteCard=Resources.Load<Sprite>("Cards/"+cardName);
        imageCard.sprite=spriteCard;

        Hand=GameObject.Find("Hand");
        GameObject EnemyPlayArea=GameObject.Find("EnemyPlayArea");
        //Si la carta esta en mano que este del lado bueno
        if (this.transform.parent==Hand.transform.parent)
        {
            cardBack=false;
        }
        staticCardBack=cardBack;

        if (this.tag=="Clone")
        {
            displayCard[0]=PlayerDeck.staticDeck[numberOfCardsInDeck-1];
            numberOfCardsInDeck-=1;
            PlayerDeck.deckSize-=1;
            cardBack=false;
            this.tag="Untagged";
        }

    }
}
