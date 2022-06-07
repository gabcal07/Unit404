using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    //Prefabs
    public GameObject playerPrefab;
    public GameObject PlayerUiPrefab;
    public GameObject ennemy;
    public GameObject Boss;
    public static GameManager Instance;


    //Position of Spawn
    public float minXPlayer;
    public float minZPlayer;
    public float maxXPlayer;
    public float maxZPlayer;
    public float minXEnemy;
    public float minZEnemy;
    public float maxXEnemy;
    public float maxZEnemy;

    //Spawn Datas
    [Tooltip("Number of spawn every 10 sec")]
    public int numberOfSpawn;
    private bool IsSpawning=true;
    public int AlreadySpawned;
    public bool roundEndend;
    public int round = 1;
    private bool BossFight;

    //[NumOfSpawn,NumSpawnEveryDelay, Life, Delay]
    public float[,] eachRound = { { 25, 5, 50, 1 }, { 30, 10, 75, 2 }, { 50, 10, 100, 5 }, { 75, 20, 150, 7 }, { 100, 20, 200, 12 } };



    void Start()
    {
        if (PManager.LocalPlayerInstance == null)
        {
            Vector3 randomPosition = new Vector3(Random.Range(5, 15), 0f, Random.Range(4, 20));
            PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
            Instance = this;
            gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);

        }
        StartCoroutine(WaitThenPlay());
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
                    en.GetComponent<Target>().ChangeH1(eachRound[round - 1, 2]);
                    this.GetComponent<PhotonView>().RPC("AlreadySpawn", RpcTarget.All, 1);
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
                    this.GetComponent<PhotonView>().RPC("changeRound", RpcTarget.All);
                    this.GetComponent<PhotonView>().RPC("AlreadySpawn", RpcTarget.All,0);

                    if (round != 5)
                    {
                        roundEndend = false;
                        IsSpawning = true;
                        StartCoroutine(BtwRound());
                    }
                    else
                    {

                        roundEndend = false;
                        IsSpawning = true;
                        StartCoroutine(SpawnBoss());
                    }

                }
               
            }
        

        }
       

        
    }
    [PunRPC]
    public void changeRound()
    {
        round += 1;
    }
    [PunRPC]
    public void AlreadySpawn(int y)
    {
        if (y == 0)
        {
            AlreadySpawned = 0;
        }
        else
        {
            AlreadySpawned += 1;
        }
    }
    IEnumerator SpawnBoss()
    {
        this.gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().BossSpawn;
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(15f);
        GameObject boss=PhotonNetwork.InstantiateRoomObject(Boss.name, new Vector3(48,10,49), Quaternion.identity);
        Debug.Log(PhotonNetwork.PlayerList.Length);
        boss.GetComponent<Target>().ChangeH1(PhotonNetwork.PlayerList.Length*1000);
        GameObject.Find("Music").GetComponent<AudioSource>().Pause();
        this.gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().BossFight;
        this.gameObject.GetComponent<AudioSource>().Play();

    }
    IEnumerator WaitThenPlay()
    {
        this.gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().Prevent;
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(10f);
        GameObject.Find("Music").GetComponent<AudioSource>().Play();
        IsSpawning = false;

    }
    IEnumerator SpawnEnemies()
    {
      yield return new WaitForSeconds(eachRound[round-1,3]);
      IsSpawning = false;
    }
    IEnumerator BtwRound()
    {
        Debug.Log("Started to wait");
        yield return new WaitForSeconds(10f);
        Debug.Log("End of the wait");

        this.gameObject.GetComponent<AudioSource>().clip= GameObject.Find("AudioManager").GetComponent<AudioManager>().BtwRound;
        this.gameObject.GetComponent<AudioSource>().Play();
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