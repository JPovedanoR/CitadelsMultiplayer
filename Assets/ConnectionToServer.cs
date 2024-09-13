using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        print("Connecting to server");
        PhotonNetwork.GameVersion="0.5";
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster(){
        print("Connected to server");
    }
    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause){
        print("Disonnected from server:"+ cause.ToString());
    }   
    public void Join2PlayerRoom(){
        PhotonNetwork.NickName="player"+Random.Range(0,9999);
        Debug.Log(PhotonNetwork.NickName);
        PhotonNetwork.JoinRandomRoom();
           
    }
    public override void OnJoinedRoom(){
        ChangeScene();
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer){
        ChangeScene();
    }
    public void ChangeScene(){
        int jugadores=PhotonNetwork.CurrentRoom.PlayerCount;
        print("jug"+jugadores);
        Debug.Log("Jugadores:"+jugadores);
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        if(jugadores==2){
            Debug.Log("Jugadores:"+jugadores);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
    public void ExitRoom(){
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom(){
        SceneManager.LoadScene(0);
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer){
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
    public override void OnJoinRandomFailed(short returnCode, string message){
        PhotonNetwork.JoinRandomOrCreateRoom(null,2);   
    }
}
