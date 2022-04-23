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

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Vector3 randomPosition = new Vector3(Random.Range(5, 15), 0.2f, Random.Range(4, 20));
            PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
            Instance = this;

        }
    }
    

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }
    public void SpawnEnemies()
    {
        int test = Random.Range(0, 600);
        if (test == 100)
        {
            for (int i = 0; i < numberOfSpawn; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(minXEnemy, maxXEnemy), 0, Random.Range(minZEnemy, maxZEnemy));
                PhotonNetwork.Instantiate(ennemy.name, randomPosition, Quaternion.identity);
            }
        }
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