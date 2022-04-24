using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Target : MonoBehaviour
{
    public float health = 50f;
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
        spawner.GetComponent<GameManager>().Kill(gameObject);

            }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health > 0)
        {
            gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
        }
          
    }

    
}
