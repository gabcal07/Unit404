using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject PlayerUiPrefab;
    public GameObject ennemy;
    // Start is called before the first frame update
    public float minXPlayer;
    public float minZPlayer;
    public float maxXPlayer;
    public float maxZPlayer;
    public static GameManager Instance;
    public float minXEnemy;
    public float minZEnemy;
    public float maxXEnemy;
    public float maxZEnemy;
    [Tooltip("Number of spawn every 10 sec")]
    public int numberOfSpawn;
    private bool IsSpawning;
    public int AlreadySpawned;
    public bool roundEndend;
    public int round = 1;
    //[NumOfSpawn,NumSpawnEveryDelay, Life, Delay]
    public float[,] eachRound = { { 25, 5, 50, 1 }, { 30, 10, 75, 2 }, { 50, 10, 100, 5 }, { 100, 20, 150, 7 }, { 200, 40, 200, 12 } };
    // Start is called before the first frame update
    void Start()
    {
        if (PManager.LocalPlayerInstance == null)
        {
            Vector3 randomPosition = new Vector3(Random.Range(5, 15), 0.2f, Random.Range(4, 20));
            PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
            Instance = this;
            gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);

        }
        IsSpawning = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        { 
            if (!IsSpawning && !roundEndend)
            {
                IsSpawning = true;
                StartCoroutine(SpawnEnemies());

                for (int i = 0; i < eachRound[round-1,1]; i++)
                {
                    Vector3 randomPosition = new Vector3(Random.Range(minXEnemy, maxXEnemy), 0, Random.Range(minZEnemy, maxZEnemy));
                    GameObject en = PhotonNetwork.InstantiateRoomObject (ennemy.name, randomPosition, Quaternion.identity);
                    en.GetComponent<Target>().health = eachRound[round - 1, 2];
                    en.GetComponent<Target>().maxHp = eachRound[round - 1, 2];
                    AlreadySpawned += 1;
                    en.transform.parent = this.gameObject.transform;
                    en.GetComponent<Target>().spawner = this.gameObject;
                }
            }
            if (AlreadySpawned >= eachRound[round-1,0])
            {
                roundEndend = true;
                if (this.gameObject.transform.childCount == 0)
                {
                    Debug.Log("Fin du round");
                    round += 1;
                    AlreadySpawned = 0;
                    roundEndend = false;
                    IsSpawning = true;
                    StartCoroutine(BtwRound());

                }
               
            }
        

        }
       

        
    }
    IEnumerator SpawnEnemies()
    {
      yield return new WaitForSeconds(eachRound[round-1,3]);
      IsSpawning = false;
    }
    IEnumerator BtwRound()
    {
        yield return new WaitForSeconds(10f);
        IsSpawning = false;
    }
    public void LeaveR()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
    public void Die(GameObject player)
    {
        player.SetActive(false);
    }
    public void Kill(GameObject en)
    {
        if (en.GetComponent<PhotonView>() != null)
        {

            if (en.GetComponent<Target>().health <= 0)
            {
                PhotonNetwork.Destroy(en);
            }
        }

    }

 
}
/*MissingReferenceException: The object of type 'PhotonView' has been destroyed but you are still trying to access it.
Your script should either check if it is null or you should not destroy the object.
Photon.Pun.PhotonNetwork.LocalCleanupAnythingInstantiated (System.Boolean destroyInstantiatedGameObjects) (at Assets/Ronan Multi/Photon/PhotonUnityNetworking/Code/PhotonNetworkPart.cs:302)
Photon.Pun.PhotonNetwork.LeftRoomCleanup () (at Assets/Ronan Multi/Photon/PhotonUnityNetworking/Code/PhotonNetworkPart.cs:277)
Photon.Pun.PhotonNetwork.OnClientStateChanged (Photon.Realtime.ClientState previousState, Photon.Realtime.ClientState state) (at Assets/Ronan Multi/Photon/PhotonUnityNetworking/Code/PhotonNetworkPart.cs:2500)
Photon.Realtime.LoadBalancingClient.set_State (Photon.Realtime.ClientState value) (at Assets/Ronan Multi/Photon/PhotonRealtime/Code/LoadBalancingClient.cs:452)
Photon.Realtime.LoadBalancingClient.Disconnect (Photon.Realtime.DisconnectCause cause) (at Assets/Ronan Multi/Photon/PhotonRealtime/Code/LoadBalancingClient.cs:1280)
Photon.Realtime.ConnectionHandler.OnDisable () (at Assets/Ronan Multi/Photon/PhotonRealtime/Code/ConnectionHandler.cs:115)
Photon.Pun.PhotonHandler.OnDisable () (at Assets/Ronan Multi/Photon/PhotonUnityNetworking/Code/PhotonHandler.cs:131)
*/