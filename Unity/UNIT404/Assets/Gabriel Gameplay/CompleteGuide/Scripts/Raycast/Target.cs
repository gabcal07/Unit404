using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float maxHp= 50f;
    public GameObject spawner;
    public PhotonView view;
    public GameObject obj;
    private void Start()
    {
        view = this.GetComponent<PhotonView>();
        spawner = GameObject.Find("GameManager");
        gameObject.transform.SetParent(spawner.transform);
    }
    void Update()
    {

    }

    [PunRPC]
    public void ChangeH(float x)
    {
        this.health = x;
    }

    public void ChangeH1(float x)
    {
        this.gameObject.GetComponent<PhotonView>().RPC("ChangeH", RpcTarget.All, x);
    }

  
    public void TakeDamage(float amount)
    {
        if (view != null)
        {
            view.RPC("TakeD", RpcTarget.All, amount);
        }

    }
    [PunRPC]
    public void TakeD(float amount)
    {
        if (amount > health)
        {
            if (view != null)
            {
                gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
            health -= amount;
            }
        }
        else
        {
            health -= amount;
            if (health > 0)
            {
                if (view != null)
                {
                    gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                }
            }
        }
        spawner.GetComponent<GameManager>().Kill(gameObject);

    }


}
