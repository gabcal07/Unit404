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
    public bool dead = false;
    public float SpawnTime = 5;
    private bool launched = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PManager>().Health <= 0 && !dead && !launched)
        {
            launched = !launched;
            //Player.GetComponent<PhotonView>().RPC("deadOrNot", RpcTarget.All);
            dead = !dead;
            PhotonNetwork.RunRpcCoroutines = true;
            StartCoroutine(res());
        }
        
    }
    public void LeaveR()
    {
        this.gameObject.GetComponentInParent<RemoveFromTheList>().remove();
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
        yield return new WaitForSeconds(SpawnTime);
        Player.transform.position = new Vector3(9, 0.2f, 33);
        Player.GetComponent<PManager>().changeHealth(200);
        SpawnParticles.transform.position = Player.transform.position;
        SpawnParticles.SetActive(true);
        SpawnParticles.GetComponent<SpawnEffect>().enabled = true;
        SpawnParticles.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        Player.SetActive(true);
        dead = !dead;
        //Player.GetComponent<PhotonView>().RPC("deadOrNot", RpcTarget.All);
        launched = !launched;

    }

    [PunRPC]
    public void deadOrNot()
    {
        dead = !dead;
    }

    [PunRPC]
    public void active(GameObject p, bool b)
    {
        p.SetActive(b);
    }
}
