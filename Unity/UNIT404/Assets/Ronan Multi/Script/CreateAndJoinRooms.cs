using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public GameObject createRoomInput;
    public GameObject joinInput;
    public GameObject playerPrefab;
    public TMP_InputField input;
    // Start is called before the first frame update
    public void CreateRoom()
    {
        if (input.text == null || input.text == "")
        {
            PhotonNetwork.CreateRoom("jeux");

        }
        else
        {
            PhotonNetwork.CreateRoom(input.text);
        }
        
        
    }
    public void JoinRoom()
    {
        if (input.text == null || input.text == "")
        {
            PhotonNetwork.JoinRoom("jeux");
            
        }
        else
        {
            PhotonNetwork.JoinRoom(input.text);
       
        }

       
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainMap2");
        PhotonNetwork.NickName = "Player " + (PhotonNetwork.CountOfPlayersInRooms + 1).ToString();
        Debug.Log("Player " + (PhotonNetwork.CountOfPlayersInRooms + 1).ToString());


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void quitdefoula()
    {
        Application.Quit();
    }
}
