using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

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
        this.gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().tp;
        this.gameObject.GetComponent<AudioSource>().mute = false;
        this.gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(wait());
        
    }

    IEnumerator wait()
    {
        
        yield return new WaitForSeconds(5f);
        //foreach (GameObject p in this.gameObject.GetComponent<GameManager>().playerList)
        //{


          //  p.<SpawnEffect>().enabled = true;
       // }
        
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LoadLevel(Scene);
        Debug.Log(PhotonNetwork.LevelLoadingProgress);
    }
}
