using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
  
{
    public GameObject playerPrefab;
    public GameObject ennemy;
    // Start is called before the first frame update
    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
