using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public class DropZone : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject PlayArea;
    private int numberOfCards;
    private int points;
    private bool ended=false;
    public static bool cartaJugada;
    public Coin dinero;
    public void OnPointerEnter(PointerEventData eventData){
        
        
    }
    public void OnDrop(PointerEventData eventData){
        int cardCost=Int32.Parse(eventData.pointerDrag.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text);
        
        if(eventData.pointerDrag !=null 
        && numberOfCards<7 
        && cardCost<=dinero.GetCoins() 
        && IsNameInPlayArea(eventData.pointerDrag.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text)
        && !cartaJugada
        )
        {
            setPlayedCardToTrue();
            PlayArea=GameObject.Find("PlayArea");
            eventData.pointerDrag.transform.parent=PlayArea.transform;
            eventData.pointerDrag.transform.localScale=Vector3.one;
            eventData.pointerDrag.transform.position=new Vector3(transform.position.x,transform.position.y,-48);
            eventData.pointerDrag.transform.eulerAngles=new Vector3(25,0,0);
            for (int i=1;i<PlayArea.transform.childCount;i++){ 
                    //Robo especial por carta nº 36 (devuelve una moneda al jugar un distrito)
                    if(36==int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1])){
                        dinero.SetCoins(cardCost-1);
                        return;
                    }
            }
            dinero.SetCoins(cardCost);
        }else{
            GameObject Hand=GameObject.Find("Hand");
            eventData.pointerDrag.transform.SetParent(Hand.transform);            
        }      
    }
    public void OnPointerExit(PointerEventData eventData){
        
    }

    public bool IsNameInPlayArea(String cardName){
        PlayArea=GameObject.Find("PlayArea");
        for(int i=0; i<numberOfCards; i++){
            if(22==int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1])){
                return true;
            }
        }
        for(int i=0; i<numberOfCards; i++){
            if(cardName.Equals(PlayArea.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text)){
                return false;
            }
            
        }
        return true;

    }
    public static void setPlayedCardToFalse(){
        cartaJugada=false;
    }    
    public static void setPlayedCardToTrue(){
        cartaJugada=true;
    } 

    // Start is called before the first frame update
    void Start()
    {
        dinero = FindObjectOfType(typeof(Coin)) as Coin;
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
}
