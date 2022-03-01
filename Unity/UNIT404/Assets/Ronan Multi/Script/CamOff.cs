using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CamOff : MonoBehaviour
{ 
    PhotonView view;
    public Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            camera.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
