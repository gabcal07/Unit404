using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnEnnemies: MonoBehaviour
  
{

    public GameObject ennemy;
    // Start is called before the first frame update
    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
            PhotonNetwork.Instantiate(ennemy.name, randomPosition, Quaternion.identity);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
