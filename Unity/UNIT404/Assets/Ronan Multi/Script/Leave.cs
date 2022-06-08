using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Leave : MonoBehaviour
{
    public GameObject Player;
    public GameObject explosion;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PManager>().Health <= 0 && !dead)
        {

            explosion.GetComponent<ParticleSystem>().Play();
            dead = !dead;
        }
        
    }
    public void LeaveR()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

}
