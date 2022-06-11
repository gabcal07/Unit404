using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
   /* private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }*/
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

    }
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Main MenuR");
    }

}
