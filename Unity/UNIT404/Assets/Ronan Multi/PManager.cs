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
}
