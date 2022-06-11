using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckDeath : MonoBehaviour
{
    public GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (AllMyFriendAreDead())
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(0);
            //foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            //{
              //  PhotonNetwork.CloseConnection(p);
            //}
        }
        
    }

    public bool AllMyFriendAreDead() 
    {
        if (manager.GetComponent<GameManager>().playerList.Count != 0)
        {
            foreach (GameObject p in manager.GetComponent<GameManager>().playerList)
            {
                Debug.Log("Is dead:" + p.GetComponentInChildren<Leave>().dead);
                if (!p.GetComponentInChildren<Leave>().dead)
                {
                    Debug.Log("CheackDeath: return false");
                    return false;

                }
            }
            Debug.Log("CheackDeath: return true");
            return true;
        }
        return false;

    }

    [PunRPC]
    public void KickPlayers()
    {
        PhotonNetwork.LeaveRoom();
    }
}
