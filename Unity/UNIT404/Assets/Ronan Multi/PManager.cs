using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PManager : MonoBehaviourPunCallbacks
{
    public int Health;
    public static GameObject LocalPlayerInstance;
    public GameObject source;
    
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
        this.gameObject.GetComponentInParent<RemoveFromTheList>().remove();
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

    public void damage(int i)
    {
        source.GetComponent<AudioSource>().PlayOneShot(source.GetComponent<AudioManager>().damage);
        this.gameObject.GetComponent<PhotonView>().RPC("damageRPC", RpcTarget.AllViaServer, i);
    }

    [PunRPC]
    public void damageRPC(int i)
    {
        if (Health - i < 0)
        {
            Health = 0;
        }
        else
        {
            Health -= i;

        }
    }
}
