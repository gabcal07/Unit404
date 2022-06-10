using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PManager : MonoBehaviourPunCallbacks
{
    public int Health;
    public static GameObject LocalPlayerInstance;
    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = 200;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
    public void changeHealth(int i)
    {
        this.gameObject.GetComponent<PhotonView>().RPC("regen", RpcTarget.AllViaServer, i);
    }

    [PunRPC]
    void regen(int i)
    {
        this.Health = i;
    }
}
