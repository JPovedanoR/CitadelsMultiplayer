using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> Deck=new List<Card>();
    public List<Card> container=new List<Card>();
    private int x;
    public static int deckSize;
    public static List<Card> staticDeck=new List<Card>();

    public GameObject cardInDeck1;
    public GameObject cardInDeck2;
    public GameObject cardInDeck3;
    public GameObject cardInDeck4;

    public GameObject CardToHand;
    public GameObject[] Clones;
    public GameObject Hand; 

    // Start is called before the first frame update
    void Start()
    {
        deckSize=40;
        //Crea el mazo
        x=0;
        //int y=0;
        for (int i = 0; i < 80; i++)
        {     
            //y=UnityEngine.Random.Range(1,40);      
            Deck.Add(CardDatabase.cardList[i]);
            x++;
        }
        
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        staticDeck=Deck;


        if (deckSize<30)
        {
            cardInDeck4.SetActive(false);
        }
        if (deckSize<20)
        {
            cardInDeck3.SetActive(false);
        }
        if (deckSize<10)
        {
            cardInDeck2.SetActive(false);
        }
        if (deckSize==0)
        {
            cardInDeck1.SetActive(false);
        }


    }


    IEnumerator StartGame(){
        Shuffle();
        
    }
    public void Shuffle(){
        //Debug.Log(String.Format("bbbb{0}",Deck[0].cardName));
        for (int i = 0; i < Deck.Count; i++)
        {          
            int randomIndex=UnityEngine.Random.Range(0,this.Deck.Count);
            container.Add(Deck[randomIndex]);
            Deck.RemoveAt(randomIndex);
        }
        Deck=container;
    }

    
}
