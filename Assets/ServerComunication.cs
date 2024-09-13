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
            if(PlayArea.transform.childCount>0){
                for (int i=0;i<=PlayArea.transform.childCount-1;i++)
                {   
                    if(PlayArea.transform.GetChild(i).gameObject.activeInHierarchy){
                        cartas[i]=int.Parse(PlayArea.transform.GetChild(i).transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text.Split('-')[1]);
                    }
                    else{
                        cartas[i]=-1;
                    }
                }
            }else{
                for (int i=0; i<cartas.Length; i++){
                    cartas[i]=-1;
                }
            }
            stream.SendNext(cartas);//cartas en mesa


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
                        Card carta=CardDatabase.cardList[cartasNew[i]];
                        EnemyPlayArea.transform.GetChild(i).gameObject.SetActive(true);                   
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
