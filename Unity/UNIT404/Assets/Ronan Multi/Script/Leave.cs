using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Leave : MonoBehaviour
{
    public GameObject Player;
    public GameObject explosion;
    public GameObject SpawnParticles;
    public bool Neige;
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
            dead = true;
            PhotonNetwork.RunRpcCoroutines = true;
            this.gameObject.GetComponent<PhotonView>().RPC("res", RpcTarget.All);
        }
        
    }
    public void LeaveR()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
 
    [PunRPC]
    public IEnumerator res()
    {
        explosion.transform.position =new Vector3 (Player.transform.position.x, Player.transform.position.y+1f, Player.transform.position.z);
        explosion.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSecondsRealtime(0.25f);
        Player.SetActive(false);        
        yield return new WaitForSeconds(5f);
        Player.transform.position = new Vector3(9, 0.2f, 33);
        Player.GetComponent<PManager>().changeHealth(200);
        SpawnParticles.transform.position = Player.transform.position;
        SpawnParticles.SetActive(true);
        SpawnParticles.GetComponent<SpawnEffect>().enabled = true;
        SpawnParticles.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        Player.SetActive(true);
        dead = false;

    }
}
