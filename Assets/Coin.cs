using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Coin : MonoBehaviour
{

    public Button coinButton;
    public Button drawButton;
    public Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        coinText.text=3.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(coinButton.isActiveAndEnabled){
            if(Input.GetKeyUp(KeyCode.C)){
                EarnCoins();
            }
        }        
    }
    public void EarnCoins(){
        int finalcoin=Int32.Parse(coinText.text);
        finalcoin=finalcoin+2;
        coinText.text=finalcoin.ToString();
        coinButton.interactable=false;
        drawButton.enabled=false;
        GameObject PlayArea=GameObject.Find("PlayArea");
        for (int i=1;i<PlayArea.transform.childCount;i++){ 
                //Robo especial por carta nº 37 (puedes robar carta Y monedas en un mismo turno)
                if(37==int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1])){
                    drawButton.enabled=true;                  
                }
        } 
    }
    public int GetCoins(){
        int devolver=Int32.Parse(coinText.text);       
        return devolver;
    }
    public void SetCoins(int coin){
        coin=GetCoins()-coin;
        coinText.text=coin.ToString();
    }
    public void EnableCoinButton(){
        coinButton.enabled=true;
    }
    public void DisableCoinButton(){
        coinButton.enabled=false;
    }
}
