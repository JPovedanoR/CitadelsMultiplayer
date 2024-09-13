using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class TurnSystem : MonoBehaviourPunCallbacks
{
    public bool isYourTurn;
    public int yourTurn;
    public Text turnText;

    public int currentCoins;
    public Button coinButton;
    public Text coinText;
    public GameObject cardToHand;
    public GameObject cardToChoose;
    public Button drawButton;
    public Button leftDraw;
    public Button rightDraw;
    public Button nextTurn;
    private Coin dinero;
    private int gamemode=0;
    private int currentTurnID;
    private int numberOfCards;
    private bool ended=false;
    private bool enteredended=false;
    public GameObject PanelWon;
    public GameObject PanelLose;
    public GameObject PlayBlocker;
    // Start is called before the first frame update
    void Start()
    {   
        //SceneManager.LoadScene("MainMenu");
        gamemode=SceneManager.GetActiveScene().buildIndex-1;

        if(gamemode==0){
            isYourTurn=true;
            yourTurn=1;
            DrawStart();
            drawButton.interactable=true;
            drawButton.enabled=true;
            leftDraw.interactable=false;
            rightDraw.interactable=false;
            //Set coin value       
            coinText.text=3.ToString();
            coinButton.interactable=true;
            //
        }
        if(gamemode==1){
            DrawStart();
            photonView.RPC(nameof(ChangeTurnOnMaster),RpcTarget.AllBuffered);
            photonView.RequestOwnership();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isYourTurn){
            turnText.text="Your Turn";
        }else{
            turnText.text="Opponent Turn";
        }
        if(drawButton.enabled==true){
            if(Input.GetKeyUp(KeyCode.R)){
                Draw();
            
            }
        }
        
        currentCoins=Int32.Parse(coinText.text);
    }


    public void EndTurn(){
        //SinglePlayer
        
        if(gamemode==0){
            GameObject PlayArea=GameObject.Find("PlayArea");
            numberOfCards=PlayArea.transform.childCount;
            Debug.Log(numberOfCards);
            if(numberOfCards==7){
                ended=true;  
                Debug.Log("7cartas");
            }
            if(ended){
                EndGame();
                Debug.Log("finaal");
            }
            
            yourTurn+=1;           
            
            for (int i=1;i<PlayArea.transform.childCount;i++){ 
                //Robo especial por carta nº 24 (roba una moneda al comenzar turno)
                if(24==int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1])){
                    coinText.text=(Int32.Parse(coinText.text)+1).ToString();
                }
                //Robo especial por carta nº 35 (roba dos monedas si al comienzo de turno no tienes)
                if(35==int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1])){
                    if(currentCoins==0){
                        coinText.text=(Int32.Parse(coinText.text)+2).ToString();        
                    }
                    
                }
            }
            ///
            GameObject Hand=GameObject.Find("Hand");           
            if(Hand.transform.childCount+1<=10){
                ButtonDrawChange(true);
            }
            coinButton.interactable=true;     
            DropZone.setPlayedCardToFalse();    
        }
        //2 Players
        if(gamemode==1){
            if(isYourTurn){//Cuando termina tu turno
                GameObject PlayArea=GameObject.Find("PlayArea");
                numberOfCards=PlayArea.transform.childCount;
                
                isYourTurn=false;
                coinButton.interactable=false;
                ButtonDrawChange(false);
                photonView.RPC("ChangeTurnOnMaster",RpcTarget.MasterClient);

                
            }else{//cuando empieza tu turno
                
            ///
            }
        }      
    }

    [PunRPC]
    private void ChangeTurnOnMaster(){
        if(!PhotonNetwork.IsMasterClient) return;

        // Get the next actor number
        // This wraps around so after reaching the last player it will again start with the first one
        currentTurnID = PhotonNetwork.LocalPlayer.GetNextFor(currentTurnID).ActorNumber;

        // Tell everyone the new active player ID
        photonView.RPC("ChangeTurn", RpcTarget.All, currentTurnID);
    }
    [PunRPC]
    private void ChangeTurn(int newID)
    {
        currentTurnID = newID;

        //Comprobar si eres el jugador activo
        if(PhotonNetwork.LocalPlayer.ActorNumber == newID){
            

            Debug.Log(numberOfCards+"cartasssss");           
                if(ended){
                    nextTurn.interactable=false;
                    Debug.Log("termina la partida");
                    EndGame();
                }
                
                
                isYourTurn=true;
                currentCoins+=2;
                GameObject Hand=GameObject.Find("Hand");
                GameObject PlayArea=GameObject.Find("PlayArea"); 
                PlayArea.SetActive(true);
                if(Hand.transform.childCount+1<=10){
                    ButtonDrawChange(true);
                }
                coinButton.interactable=true;

                for (int i=1;i<PlayArea.transform.childCount;i++){ 
                    //Robo especial por carta nº 24 (roba una moneda al comenzar turno)
                    if(24==int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1])){
                        coinText.text=(Int32.Parse(coinText.text)+1).ToString();
                    }
                    //Robo especial por carta nº 35 (roba dos monedas si al comienzo de turno no tienes)
                    if(35==int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1])){
                        if(currentCoins==0){
                            coinText.text=(Int32.Parse(coinText.text)+2).ToString();        
                        }                 
                    }
                }
            isYourTurn=true;
            coinButton.interactable=true;
            ButtonDrawChange(true);
            PlayBlocker.SetActive(false);
            nextTurn.interactable=true;
            DropZone.setPlayedCardToFalse();
        }else{
            if(numberOfCards==7){
                    ended=true;        
            }
            isYourTurn=false;
            coinButton.interactable=false;
            ButtonDrawChange(false);
            PlayBlocker.SetActive(true);
            nextTurn.interactable=false;
        }
    }
    
    public void EndOpponentTurn(){
        isYourTurn=true;
        yourTurn+=1;
        currentCoins+=2;
        GameObject Hand=GameObject.Find("Hand");
        Debug.Log(Hand.transform.childCount);
        if(Hand.transform.childCount+1<=10){
            Debug.Log(Hand.transform.childCount);
            ButtonDrawChange(true);
        }
        coinButton.interactable=true;
    }

    public void DrawStart(){   
        StartCoroutine(DrawStart(4));
        drawButton.enabled=true;

    }

    IEnumerator DrawStart(int card){
        for (int i = 0; i < card; i++)
        {
            yield return new WaitForSeconds(0.25f);
            Instantiate(cardToHand,transform.position,transform.rotation);
        }      
    }
    public void Draw(){
        if(CanDraw()){    
            StartCoroutine(Draw(2));
            ButtonDrawChange(false);
            coinButton.interactable=false;
            leftDraw.interactable=true;
            rightDraw.interactable=true;
            GameObject PlayArea=GameObject.Find("PlayArea");
            for (int i=1;i<PlayArea.transform.childCount;i++){ 
                    //Robo especial por carta nº 37 (puedes robar carta Y monedas en un mismo turno)
                    if(37==int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1])){
                        coinButton.interactable=true;                       
                    }
            }            
        }
    }
    IEnumerator Draw(int card){
        for (int i = 0; i < card; i++)
        {
            yield return new WaitForSeconds(0.01f);
            Instantiate(cardToChoose,transform.position,transform.rotation);
        }      
    }
    public void ButtonDrawChange(bool isActive){
        drawButton.enabled=isActive;     
    }
    public static bool CanDraw(){
        GameObject Hand=GameObject.Find("Hand");
        if(Hand.transform.childCount+2>10){
            return false;
        }else return true;  
    }    

    [PunRPC]
    public void EndGame(){
        int myPoints=0;
        int enemyPoints=0;
        
        GameObject PlayArea=GameObject.Find("PlayArea");
        myPoints=EndPoints(PlayArea);

        if(gamemode==1){
            //Solo el primer jugador que llegue a 7 cumplira la condicion del if
            if(ended && !enteredended){
                enteredended=true;
                myPoints+=2;
                photonView.RPC("EndGame", RpcTarget.OthersBuffered);//avisa al otro jugador de que la partida ha terminado
            }
            
            GameObject EnemyPlayArea=GameObject.Find("EnemyPlayArea");
            enemyPoints=EndPoints(EnemyPlayArea);

            if(myPoints>enemyPoints){
                //PanelWon=GameObject.Find("EndPanelWon");
                PanelWon.SetActive(true);
                PanelWon.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text="You earned "+myPoints.ToString()+" points!";
                PanelWon.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text="Your enemy earned "+enemyPoints.ToString()+" points!";
            }
            if(myPoints<enemyPoints){
                //PanelLose=GameObject.Find("EndPanelLose");
                PanelLose.SetActive(true);
                PanelLose.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text="You earned "+myPoints.ToString()+" points!";
                PanelLose.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text="Your enemy earned "+enemyPoints.ToString()+" points!";
            }
            if(myPoints==enemyPoints){
                //PanelWon=GameObject.Find("EndPanelWon");
                PanelWon.SetActive(true);
                PanelWon.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text="You earned "+myPoints.ToString()+" points!";
                PanelWon.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text="Your enemy earned "+enemyPoints.ToString()+" points!";
                
            }
        }else{
            //PanelWon=GameObject.Find("EndPanelWon");
            PanelWon.SetActive(true);
            PanelWon.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text="You earned "+myPoints.ToString()+" points!";
        }

        
        
    }  
    public int EndPoints(GameObject playArea){
        int points=0;
        bool hay7=false;
        bool hay33=false;

        List<bool> fullCity=new List<bool>(5);
        fullCity.Add(false);
        fullCity.Add(false);
        fullCity.Add(false);
        fullCity.Add(false);
        fullCity.Add(false);

        for (int i = 0; i < numberOfCards; i++){
            //7 20 21 23 26 33 39 CARTAS CON EFECTOS ESPECIALES AL FINAL DE PARTIDA
            string[] numeroCartaString=playArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-');
            int numeroCarta=int.Parse(numeroCartaString[1]);
            /// 
            switch (numeroCarta)
            {
                case 7:
                    hay7=true;
                    break;
                case 20:
                    points+=2;
                    break;
                case 21:
                    points+=2;
                    break;
                case 23:         
                    GameObject Hand=GameObject.Find("Hand");        
                    points+=Hand.gameObject.transform.childCount-1;
                    break;
                case 26:
                    points+=dinero.GetCoins();
                    break;
                case 33:
                    hay33=true;
                    break;
                case 39: //esta carta y la 7 son la misma pero con diferente id
                    hay7=true;
                    break;
                default:
                    break;
            }
            //////
            
            ///JUNTAMOS LOS TIPOS DE DISTRITOS
            for (int j=0; j<5; j++)
            {
                fullCity[int.Parse(playArea.transform.GetChild(i).GetChild(0).GetChild(4).GetChild(0).GetComponent<UnityEngine.UI.Text>().text)]=true;
            }  
            ///
            
            ///SUMA LOS COSTES DE CADA DISTRITO JUGADO
            points+=int.Parse(playArea.transform.GetChild(i).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text);
        }
        if(hay7){
            for (int i=0; i<numberOfCards; i++){
                if(4==int.Parse(playArea.transform.GetChild(i).GetChild(0).GetChild(4).GetChild(0).GetComponent<UnityEngine.UI.Text>().text)){
                    points++;
                }
            }
        }

        //SUMA DISTRITOS DIFERENTES
        int differentDistricts=0;
        foreach (bool district in fullCity)
        {
            if(district==true)differentDistricts++;
        }
        if(differentDistricts==5 || (differentDistricts==4 && hay33)) points+=3;
        ///
        Debug.Log("FINAL");
        Debug.Log(points);
        return points;
    }
    public void BackToMenu(){
        if(gamemode==1){
            PhotonNetwork.LeaveRoom();
        }        
        SceneManager.LoadScene(0);
    }
}
