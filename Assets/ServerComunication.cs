using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon;

public class ServerComunication : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject Hand;
    public GameObject EnemyHand;
    public GameObject PlayArea;
    public GameObject EnemyPlayArea;
    public Text coinText;
    public Text enemyCoinText;
    public GameObject enemyCard;

    
    private int numberEnemyCards=0;
    // Start is called before the first frame update
    void Start()
    {
        Hand=GameObject.Find("Hand");
        EnemyHand=GameObject.Find("EnemyHand");
        PlayArea=GameObject.Find("PlayArea");
        EnemyPlayArea=GameObject.Find("EnemyPlayArea");
        PhotonNetwork.SerializationRate=5;
        //PhotonView pv =GetComponent<PhotonView>();
        //if (pv) pv.ObservedComponents.Add(this);


        //Instantiate(enemyCard,EnemyHand.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            int[] cartas=new int[7];
            for(int i=0;i<cartas.Length;i++){
                cartas[i]=-1;
            }
            stream.SendNext(Hand.transform.childCount-1);//tamaño de mano
            stream.SendNext(coinText.text);//monedas
            //Debug.Log(PlayArea.transform.childCount+"valoorrrrrrrrrrrrrrrrrrrrr");
            if(PlayArea.transform.childCount>0){
                //Debug.Log(PlayArea.transform.childCount+"ysemeteconsupolla");
                //Debug.Log(PlayArea.transform.childCount);
                //Debug.Log(PlayArea.transform.GetChild(0).transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1]);
                for (int i=0;i<=PlayArea.transform.childCount-1;i++)
                {   
                    if(PlayArea.transform.GetChild(i).gameObject.activeInHierarchy){
                        cartas[i]=int.Parse(PlayArea.transform.GetChild(i).transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1]);
                        //Debug.Log("dentro");
                        //Debug.Log(PlayArea.transform.GetChild(i).transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1]);
                    }
                    else{
                        //Debug.Log("el-1");
                        cartas[i]=-1;
                    }
                    //cartas.Add(int.Parse(PlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1]));
                    
                    //cartas.Add(PlayArea.transform.GetChild(i).gameObject);
                }
            }else{
                for (int i=0; i<cartas.Length; i++){
                    cartas[i]=-1;
                }
            }
            stream.SendNext(cartas);//cartas en mesa
            //Debug.Log(cartas[0]+"ccc"+cartas[1]+"ccc"+cartas[2]+"ccc"+cartas[3]+"ccc"+cartas[4]);


        }else if(stream.IsReading){
            numberEnemyCards=(int)stream.ReceiveNext();
            if(numberEnemyCards!=EnemyHand.transform.childCount){
                for (int i=0; i<EnemyHand.transform.childCount; i++)
                {
                    Destroy(EnemyHand.transform.GetChild(i).gameObject);
                }
                for (int i=0; i<numberEnemyCards; i++)
                {
                    Instantiate(enemyCard,EnemyHand.transform);
                }
            }
            enemyCoinText.text=(string)stream.ReceiveNext();
            int[] cartasNew=new int[7];
            cartasNew=(int[])stream.ReceiveNext(); 
            if(cartasNew[0]!=-1){                
                for (int i = 0; i < cartasNew.Length; i++){
                    if(cartasNew[i]!=-1){
                        //Debug.Log(cartasNew[i]+"uuuuuuuuuuuuuuuuuuuuuuuuu");
                        Card carta=CardDatabase.cardList[cartasNew[i]];
                        //Debug.Log(carta.cardName+"oooooooooooooooooooooooooooooooooooooooooo");
                        EnemyPlayArea.transform.GetChild(i).gameObject.SetActive(true);   
                        //Debug.Log(carta.cardName+"rerere"+carta.cardDescription+"rerere"+carta.cost+"rerere"+carta.type);                    
                        EnemyPlayArea.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text=carta.cardName;//nombre
                        EnemyPlayArea.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite=Resources.Load<Sprite>("Cards/"+carta.cardName);//imagen
                        EnemyPlayArea.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text=carta.cardDescription;//descripcion                        
                        EnemyPlayArea.transform.GetChild(i).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text=carta.cost.ToString();//coste
                        EnemyPlayArea.transform.GetChild(i).GetChild(0).GetChild(4).GetChild(0).GetComponent<UnityEngine.UI.Text>().text=carta.type.ToString();//tipo
                    }                                            
                }                               
            }                        
        }
    }
}