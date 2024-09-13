using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToHand : MonoBehaviour
{
    public GameObject Hand;
    public GameObject HandCard=null;
    public int amountCards;

    // Start is called before the first frame update
    void Start()
    {
        amountCards=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(amountCards<3){
            Hand=GameObject.Find("Hand");
            HandCard.transform.SetParent(Hand.transform);
            HandCard.transform.localScale=Vector3.one;
            HandCard.transform.position=new Vector3(transform.position.x,transform.position.y);
            HandCard.transform.eulerAngles=new Vector3(25,0);
            amountCards+=1;
        }
        
    }
}
