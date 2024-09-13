using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardToChoose : MonoBehaviour
{    
    public GameObject Choose;
    public GameObject ChooseCard=null;   

    public Button LeftCard;
    
    public Button RightCard;

    // Start is called before the first frame update
    void Start()
    { 
        Choose=GameObject.Find("Choose");
        Choose.SetActive(true);  

        ChooseCard.transform.SetParent(Choose.transform);
        ChooseCard.transform.localScale=Vector3.one;
        ChooseCard.transform.position=new Vector3(transform.position.x,transform.position.y);
        ChooseCard.transform.eulerAngles=new Vector3(25,0);  
    }

    // Update is called once per frame
    void Update()
    {       
               
        /* if(Choose.transform.childCount>2){
            LeftCard.enabled=false;
            RightCard.enabled=false; 
        } */
            //Choose=GameObject.Find("Choose");      
            
            
            //ChooseCard.SetActive(false);
            //amountCards+=1;*/
    }

    public void card1Chosen(){
        //Debug.Log("");
        Choose=GameObject.Find("Choose");
        GameObject Hand=GameObject.Find("Hand");
        Transform cardToDraw=Choose.transform.GetChild(1);

        cardToDraw.parent=Hand.transform;
        cardToDraw.localScale=Vector3.one;
        cardToDraw.position=new Vector3(transform.position.x,transform.position.y,-48);
        cardToDraw.eulerAngles=new Vector3(25,0,0);
        LeftCard.interactable=false;
        RightCard.interactable=false;
        ///Robo especial por carta nº18 (roba las dos cartas en vez de elegir entre las dos)
        GameObject PlayArea=GameObject.Find("PlayArea");
        for (int i=1;i<PlayArea.transform.childCount;i++)
        {   
            
            if(18==int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1])){
                Transform cardToDrawSpecial=Choose.transform.GetChild(1);

                cardToDrawSpecial.parent=Hand.transform;
                cardToDrawSpecial.localScale=Vector3.one;
                cardToDrawSpecial.position=new Vector3(transform.position.x,transform.position.y,-48);
                cardToDrawSpecial.eulerAngles=new Vector3(25,0,0);
                return;            
            }
                
        }
        ///
        Destroy(Choose.transform.GetChild(1).gameObject); 
                


    }
    public void card2Chosen(){
        //Debug.Log("");
        Choose=GameObject.Find("Choose");
        
        GameObject Hand=GameObject.Find("Hand");
        Transform cartaARobar=Choose.transform.GetChild(2);

        cartaARobar.parent=Hand.transform;
        cartaARobar.localScale=Vector3.one;
        cartaARobar.position=new Vector3(transform.position.x,transform.position.y,-48);
        cartaARobar.eulerAngles=new Vector3(25,0,0);
        LeftCard.interactable=false;
        RightCard.interactable=false;
        ///Robo especial por carta nº18
        GameObject PlayArea=GameObject.Find("PlayArea");
        for (int i=1;i<PlayArea.transform.childCount;i++)
        {   
            
            if(18==int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1])){
                Transform cardToDrawSpecial=Choose.transform.GetChild(1);

                cardToDrawSpecial.parent=Hand.transform;
                cardToDrawSpecial.localScale=Vector3.one;
                cardToDrawSpecial.position=new Vector3(transform.position.x,transform.position.y,-48);
                cardToDrawSpecial.eulerAngles=new Vector3(25,0,0);
                return;            
            }
                
        }
        ///         
        Destroy(Choose.transform.GetChild(1).gameObject);
    }

}
