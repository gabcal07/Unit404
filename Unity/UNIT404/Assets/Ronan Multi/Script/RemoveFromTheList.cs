using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RemoveFromTheList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void remove()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().playerList.Remove(this.gameObject);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
