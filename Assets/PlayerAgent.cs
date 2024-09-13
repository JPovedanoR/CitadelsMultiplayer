using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.MLAgents;
using System;

public class PlayerAgent
{
    public GameObject CardToHand;

    /* public override void Initialize(){
        MaxStep=5000;
        
        //
    }/* 
    public override void OnEpisodeBegin(){
        GameObject Hand=GameObject.Find("Hand");
        GameObject PlayArea=GameObject.Find("PlayArea");
        Debug.Log("vacia la mano");
        for (int i=1;i<=Hand.transform.childCount-1;i++){
            string idCard =Hand.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text;
            string nameCard=Hand.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
            string costCard=Hand.transform.GetChild(i).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
            string typeCard=Hand.transform.GetChild(i).GetChild(0).GetChild(4).GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
            string descriptionCard =Hand.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text;   
            Card card=new Card(int.Parse(idCard.Split('-')[1]),nameCard,int.Parse(costCard),int.Parse(typeCard),descriptionCard);

            //PlayerDeck.staticDeck.Add(card);
            //CardDatabase.cardList.Add(card);
        }
        for (int i=1;i<=Hand.transform.childCount-1;i++)
        {
            Destroy(Hand.transform.GetChild(i).gameObject);
        } 
        
        Debug.Log("vacia la mesa");
        if(PlayArea.transform.childCount!=0){       
            for (int i=0;i<PlayArea.transform.childCount;i++){
                string idCard =PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text;
                string nameCard=PlayArea.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
                string costCard=PlayArea.transform.GetChild(i).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
                string typeCard=PlayArea.transform.GetChild(i).GetChild(0).GetChild(4).GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
                string descriptionCard =PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text;   
                Card card=new Card(int.Parse(idCard.Split('-')[1]),nameCard,int.Parse(costCard),int.Parse(typeCard),descriptionCard);

                //PlayerDeck.staticDeck.Add(card);
                //CardDatabase.cardList.Add(card);
                //Debug.Log(card+"carta");               
            }
            Debug.Log(CardDatabase.cardList.Count+"mazo");
            for (int i=0;i<=PlayArea.transform.childCount-1;i++)
            {
                Destroy(PlayArea.transform.GetChild(i).gameObject);
            }
        }
        DisplayCard.numberOfCardsInDeck=40;
        ////Queda hacer barajar el mazo despues de resetearlo
        //5 cartas iniciales      
        StartCoroutine(Draw(5));

    }
    public override void OnActionReceived(Unity.MLAgents.Actuators.ActionBuffers actions){
        int actionToTake=actions.DiscreteActions[0];//It goes from 0 to 11  /////juntar todo en un mismo discrete actions y dividirlo por indices DONE
        actionToTake++;////incrementar todos los numeros para que no cambie la probabilidad (ahora hay doble de elegir uno) DONE  //It goes from 1 to 12*
        //Debug.Log("actions"+actions.DiscreteActions.Length);
        Debug.Log("action"+actionToTake);
        Debug.Log("despues"+actions.DiscreteActions[0]);
        if(actionToTake<=10){
            GameObject Hand=GameObject.Find("Hand");
            int cardsInHand=GameObject.Find("Hand").transform.childCount-1;
            //int resta=0;


            //if(cardsInHand<actionToTake){//enmascaramiento de acciones 
            //    resta=actionToTake;
            //    actionToTake=actionToTake-cardsInHand;
            //}
            //if(cardsInHand<actionToTake){
            //    actionToTake=resta-actionToTake;
            //}           
            if(cardsInHand>0){
                agentMove(actionToTake);////añadir recompensa solo al bajar DONE
            }
            
        }
        else if(actionToTake==11){//0 dont draw, 1 draw  el valor 11 es robar
            Draw();
        }else if(actionToTake==12){//y el 12 es pasar de turno

        }
        
    }
    public override void WriteDiscreteActionMask(Unity.MLAgents.Actuators.IDiscreteActionMask actionMask){
        GameObject Hand=GameObject.Find("Hand");
        int cardsInHand=Hand.transform.childCount;
        for (int i=1;i<=cardsInHand;i++)
        {
            //Debug.Log("mascarasi");
            //Debug.Log(cardsInHand);
            actionMask.SetActionEnabled(0,i,true);
        }
        for (int i=cardsInHand+1;i<=11;i++)
        {
            //Debug.Log("mascarano"+i);
            //Debug.Log(cardsInHand);
            actionMask.SetActionEnabled(0,i,false);
        }
        actionMask.SetActionEnabled(0,10,true);
        actionMask.SetActionEnabled(0,11,true);      
    }
    public override void CollectObservations(Unity.MLAgents.Sensors.VectorSensor sensor){
        GameObject Hand=GameObject.Find("Hand");
        GameObject PlayArea=GameObject.Find("PlayArea");
        int cardsInHand=Hand.transform.childCount;

        //observartion1////////////// (1)
        sensor.AddObservation(cardsInHand);
        //Debug.Log(cardsInHand+"numero");
        /////////////////////////////
        /// (10)
        int[] values=cardValue(Hand);
        for (int  i=0; i<10; i++)
        {
            sensor.AddObservation(values[i]);
        }
        //le pasamos toda la mano (10 cartas) y si en esa posicion no hay carta, el valor de esta es 0

        //le pasamos tambien la puntuacion actual de la mesa(1)
        int puntuacion=0;
        int[] cardValues=new int[10];
        int cards=PlayArea.transform.childCount; 

        for(int i=0;i<cards;i++){
            cardValues[i]=int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text);        
        }
        if(cards<10){
            //int diff=10-cardsInHand;
            for (int i=cards;i<10;i++)
            {
                cardValues[i]=0;
            }
        }
        for (int i=0;i<cardValues.Length;i++)
        {
            puntuacion+=cardValues[i];
        }
        sensor.AddObservation(puntuacion);
        //Debug.Log(puntuacion+"puntuacion");

        
    } */
    /// <summary>
    /// Recogera en un array el valor de 10 cartas de la mano, si hay una posicion vacia, devolvera un 0 
    /// </summary>
    public int[] cardValue(GameObject gameObject){
        int[] cardValues=new int[10];
        GameObject Hand=gameObject;
        int cards=gameObject.transform.childCount; 

        for(int i=1;i<cards;i++){
            cardValues[i-1]=int.Parse(gameObject.transform.GetChild(i).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text);        
        }
        if(cards<10){
            //int diff=10-cardsInHand;
            for (int i=cards;i<10;i++)
            {
                cardValues[i]=0;
            }
        }
        return cardValues;
    }
    /* public void agentMove(int index){
        //Debug.Log("entracarta"+index);
        GameObject Hand=GameObject.Find("Hand");
        GameObject PlayArea=GameObject.Find("PlayArea");
        Debug.Log(Hand.transform.childCount+"hijos");
        Debug.Log(index+"indice");
        Transform playcard=Hand.transform.GetChild(index);
        int bonusReward=int.Parse(Hand.transform.GetChild(index).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text);
        playcard.parent=PlayArea.transform;
        playcard.localScale=Vector3.one;
        playcard.position=new Vector3(transform.position.x,transform.position.y,-48);
        playcard.eulerAngles=new Vector3(25,0,0);
        AddReward(.4f+(bonusReward/5));
        Debug.Log(GetCumulativeReward());
    } */
    /* public void Draw(){
        if(TurnSystem.canDraw()){
            StartCoroutine(Draw(2));
            //GameObject DrawButton=GameObject.Find("DrawButton");
            //DrawButton.SetActive(false);
        }
    }
    IEnumerator Draw(int card){
        //CardToHand=GameObject.Find("CardToHand");        
        Debug.Log(CardToHand);
        for (int i = 0; i < card; i++)
        {

            yield return new WaitForSeconds(1);
            Instantiate(CardToHand,transform.position,transform.rotation);

        }
    } */
    // Start is called before the first frame update
    void Start()
    {
        GameObject CardToHand =Resources.Load("CardToHand") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
