using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TP : MonoBehaviour
{
    public string Scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void tp()
    {
        StartCoroutine(wait());
        
    }

    IEnumerator wait()
    {
        this.gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().tp;
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(5f);
        //foreach (GameObject p in this.gameObject.GetComponent<GameManager>().playerList)
        //{


          //  p.<SpawnEffect>().enabled = true;
       // }
        
        yield return new WaitForSeconds(3f);
        
        PhotonNetwork.LoadLevel(Scene);
    }
}
