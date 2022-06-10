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
    [Tooltip ("Number of spawn every 10 sec")]
    public int numberOfSpawn;

    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        int test = Random.Range(0, 600);
        if(test==100)
        {
            for (int i = 0; i < numberOfSpawn; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
                PhotonNetwork.Instantiate(ennemy.name, randomPosition, Quaternion.identity);
            }
        }
    }

   
}
