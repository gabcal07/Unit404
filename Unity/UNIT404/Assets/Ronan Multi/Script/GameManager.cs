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
    public Light sun;


    //Position of Spawn
    public float minXPlayer;
    public float minZPlayer;
    public float maxXPlayer;
    public float maxZPlayer;
    public float minXEnemy;
    public float minZEnemy;
    public float maxXEnemy;
    public float maxZEnemy;
    public float y;

    //Spawn Datas
    [Tooltip("Number of spawn every 10 sec")]
    public int numberOfSpawn;
    private bool IsSpawning=true;
    public int AlreadySpawned;
    public bool roundEndend;
    public int round = 0;
    public bool BossFight;
    public bool BossSpawned;
    public List<GameObject> playerList;
    public bool Desert;

    //[NumOfSpawn,NumSpawnEveryDelay, Life, Delay]
    public float[,] eachRound = { { 30, 30, 50, 1 }, { 50, 25, 75, 2 }, { 70, 30, 100, 5 }, { 80, 40, 150, 6 }, { 1, 20, 200, 5 }};


    void Start()
    {
        if (PManager.LocalPlayerInstance == null)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minXPlayer, maxXPlayer), y, Random.Range(minZPlayer, maxZPlayer)) ;
            GameObject p= PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
            //playerList.Add(p);
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
            //Carte Neige
            if (!Desert)
            {

                if (!IsSpawning && !roundEndend && !BossFight)
                {
                    IsSpawning = true;
                    StartCoroutine(SpawnEnemies());

                    for (int i = 0; i < eachRound[round - 1, 1]; i++)
                    {
                        Vector3 randomPosition = new Vector3(Random.Range(minXEnemy, maxXEnemy), 0, Random.Range(minZEnemy, maxZEnemy));
                        GameObject en = PhotonNetwork.InstantiateRoomObject(ennemy.name, randomPosition, Quaternion.identity);
                        en.GetComponent<Target>().ChangeHealth(eachRound[round-1,2]);

                        AlreadySpawned += 1;
                        //roundEndend= AlreadySpawned>=eachRound[round]
                        en.transform.parent = this.gameObject.transform;
                        en.GetComponent<Target>().spawner = this.gameObject;
                    }
                }
                if (BossFight || AlreadySpawned >= eachRound[round-1, 0])
                {
                    roundEndend = true;
                    if (this.gameObject.transform.childCount == 0)
                    {
                        //Debug.Log("Fin du round");

                        AlreadySpawned = 0;
                        if (round == 4)
                        {
                            roundEndend = false;
                            IsSpawning = true;
                            BossFight = true;
                            this.gameObject.GetComponent<PhotonView>().RPC("nextRound", RpcTarget.All);

                            //round += 1;
                            Debug.Log("Boucle 1");
                            StartCoroutine(SpawnBoss()); ;
                        }
                        else
                        {
                            if (round != 5)
                            {
                                roundEndend = false;
                                IsSpawning = true;
                                //round += 1;
                                this.gameObject.GetComponent<PhotonView>().RPC("nextRound", RpcTarget.All);
                                StartCoroutine(BtwRound());
                            }
                        }

                    }
                    if (BossFight && this.gameObject.transform.childCount == 0 && BossSpawned)
                    {
                        this.gameObject.GetComponent<AudioSource>().Pause();
                        //this.gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().tp;
                        //this.gameObject.GetComponent<AudioSource>().Play();
                        foreach (GameObject p in playerList)
                        {
                            p.transform.Find("PlayerManager").GetComponent<AudioSource>().PlayOneShot(p.transform.Find("PlayerManager").GetComponent<AudioManager>().tp);
                        }
                        this.gameObject.GetComponent<TP>().tp();
                    }

                }
            }



            //Carte Desert
            else
            {
                float[] next = SpawnDatas(round);
                


                if (!IsSpawning && !roundEndend && !BossFight)
                {


                    IsSpawning = true;
                    StartCoroutine(SpawnEnemies());
                    //[TotalEnemy, EnemyBySpawn, HP]
                    if (AlreadySpawned + next[1] < next[0])
                    {
                        for (int i = 0; i < next[1]; i++)
                        {
                            Vector3 randomPosition = new Vector3(Random.Range(minXEnemy, maxXEnemy), 0, Random.Range(minZEnemy, maxZEnemy));
                            GameObject en = PhotonNetwork.InstantiateRoomObject(ennemy.name, randomPosition, Quaternion.identity);
                            en.GetComponent<Target>().ChangeHealth(next[2]);
                            AlreadySpawned += 1;
                            en.transform.parent = this.gameObject.transform;
                            en.GetComponent<Target>().spawner = this.gameObject;
                        }
                    }
                    else
                    {
                        float tospawn = next[0] - AlreadySpawned;
                        for (int i = 0; i < tospawn; i++)
                        {
                            Vector3 randomPosition = new Vector3(Random.Range(minXEnemy, maxXEnemy), 0, Random.Range(minZEnemy, maxZEnemy));
                            GameObject en = PhotonNetwork.InstantiateRoomObject(ennemy.name, randomPosition, Quaternion.identity);
                            en.GetComponent<Target>().ChangeHealth(next[2]);
                            AlreadySpawned += 1;
                            en.transform.parent = this.gameObject.transform;
                            en.GetComponent<Target>().spawner = this.gameObject;
                        }
                    }

                }
                if (BossFight || AlreadySpawned >= next[0])
                {
                    roundEndend = true;
                    if (this.gameObject.transform.childCount == 0)
                    {
                        //Debug.Log("Fin du round");

                        AlreadySpawned = 0;
                        if ((round) % 5 == 0)
                        {
                            roundEndend = false;
                            IsSpawning = true;
                            BossFight = true;
                            this.gameObject.GetComponent<PhotonView>().RPC("nextRound", RpcTarget.All);

                            //round += 1;
                            Debug.Log("Boucle 1");
                            StartCoroutine(SpawnBoss()); ;
                        }
                        else
                        {
                            if ((round-1)%5 != 0 || round==1)
                            {
                                Debug.Log("boucle 2");
                                roundEndend = false;
                                IsSpawning = true;
                                //round += 1;
                                this.gameObject.GetComponent<PhotonView>().RPC("nextRound", RpcTarget.All);
                                StartCoroutine(BtwRound());
                            }

                        }

                    }
                    if (BossFight && BossSpawned && this.gameObject.transform.childCount == 0 )
                    {
                        this.gameObject.GetComponent<AudioSource>().Pause();
                        GameObject.Find("Music").GetComponent<AudioSource>().Play();
                        BossFight = false;
                        BossSpawned = false;
                        roundEndend = false;
                        IsSpawning = true;
                        //round += 1;
                         this.gameObject.GetComponent<PhotonView>().RPC("nextRound", RpcTarget.All);
                         StartCoroutine(BtwRound());

                    }
                }
            }
            checkPLayerList();
        }


    }
       

           [PunRPC]
    public void nextRound()
    {
        round = round + 1;
    }
    IEnumerator SpawnBoss()
    {
        //this.gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().BossSpawn;
        //this.gameObject.GetComponent<AudioSource>().Play();
        foreach (GameObject p in playerList) 
        { 
            p.transform.Find("PlayerManager").GetComponent<AudioSource>().PlayOneShot(p.transform.Find("PlayerManager").GetComponent<AudioManager>().BossSpawn); 
        }

        //sun.color = new Color(87, 87, 87);
        yield return new WaitForSeconds(15f);
        GameObject boss=PhotonNetwork.InstantiateRoomObject(Boss.name, new Vector3(48,1,49), Quaternion.identity);
            //Debug.Log(PhotonNetwork.PlayerList.Length);
        //boss.GetComponent<Target>().ChangeHealth(1000 + 200 * round);
        boss.transform.parent = this.gameObject.transform;
        boss.GetComponent<Target>().spawner = this.gameObject;
        this.gameObject.GetComponent<PhotonView>().RPC("BossSp", RpcTarget.All);
        GameObject.Find("Music").GetComponent<AudioSource>().Pause();
        //this.gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().BossFight;
        //this.gameObject.GetComponent<AudioSource>().Play();
        foreach (GameObject p in playerList)
        {
            p.transform.Find("PlayerManager").GetComponent<AudioSource>().PlayOneShot(p.transform.Find("PlayerManager").GetComponent<AudioManager>().BossFight);
        }


    }
    IEnumerator WaitThenPlay()
    {
        // this.gameObject.GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().Prevent;
        //this.gameObject.GetComponent<AudioSource>().Play();
        foreach (GameObject p in playerList)
        {
            p.transform.Find("PlayerManager").GetComponent<AudioSource>().PlayOneShot(p.transform.Find("PlayerManager").GetComponent<AudioManager>().Prevent);
        }
        yield return new WaitForSeconds(10f);
        GameObject.Find("Music").GetComponent<AudioSource>().Play();
        IsSpawning = false;

    }
    IEnumerator SpawnEnemies()
    {
      yield return new WaitForSeconds(5f);
      IsSpawning = false;
    }
    IEnumerator BtwRound()
    {
        //Debug.Log("Started to wait");
        /*if (round == 2)
        {
            sun.color = new Color(255f, 214f, 134f);
        }
        if (round == 3)
        {
            sun.color = new Color(255f, 161f, 71f);
        }*/
        yield return new WaitForSeconds(10f);
        //Debug.Log("End of the wait");
        foreach (GameObject p in playerList)
        {
            p.GetComponentInChildren<PManager>().changeHealth(200);
        }
        //this.gameObject.GetComponent<AudioSource>().clip= GameObject.Find("AudioManager").GetComponent<AudioManager>().BtwRound;
        // this.gameObject.GetComponent<AudioSource>().Play();
        foreach (GameObject p in playerList)
        {
            p.transform.Find("PlayerManager").GetComponent<AudioSource>().PlayOneShot(p.transform.Find("PlayerManager").GetComponent<AudioManager>().BtwRound);
        }
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

    public void checkPLayerList()
    {
        if (PhotonNetwork.PlayerList.Length != playerList.Count)
        {
            foreach (GameObject g in playerList)
            {
                if (g.transform.childCount < 7)
                {
                    playerList.Remove(g);
                }
            }
        }
    }
    //[TotalEnemy, EnemyBySpawn, HP]


    public float[] SpawnDatas(int i)
    {
        
        float NBofEn = (1.24273f * (Mathf.Pow(((float)i), 1.64606f)) + 15.5843f);
        NBofEn = Mathf.Round(NBofEn);
        float Spawn = NBofEn / 4;
        if (i > 16)
        {
            
            Spawn = Mathf.Round(Spawn);
        }
        else
        {
            Spawn = NBofEn / (Mathf.Sqrt(2) * Mathf.Sqrt(i));
            Spawn = Mathf.Round(Spawn);
        }
        float HP= 1.04875f * (Mathf.Pow(((float) i), 1.71046f)) + 52.4791f;
        HP = Mathf.Round(HP);
        return new float[] {NBofEn, Spawn, HP};

    }

    [PunRPC]
    public void BossSp()
    {
        BossSpawned = !BossSpawned;
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