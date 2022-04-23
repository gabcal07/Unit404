using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public GameObject createRoomInput;
    public GameObject joinInput;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("jeu");
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("jeu");
       
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainMap2");
        
        /*PhotonNetwork.NickName = "Player " + (PhotonNetwork.CountOfPlayersInRooms + 1).ToString();
        Debug.Log("Player " + (PhotonNetwork.CountOfPlayersInRooms + 1).ToString());*/


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
