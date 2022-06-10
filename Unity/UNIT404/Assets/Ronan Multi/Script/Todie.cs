using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Todie : MonoBehaviour
{
    public GameObject player;
    public PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = player.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                player.GetComponent<PManager>().changeHealth(player.GetComponent<PManager>().Health - 50);
            }
        }
    }
}
