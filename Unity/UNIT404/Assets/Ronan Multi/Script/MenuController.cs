using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Realtime;

public class MenuController : MonoBehaviour
{
     [SerializeField] private GameObject UsernameMenu;

    [SerializeField] private GameObject ConnectPanel;

    [SerializeField]  public TextField UsernameInput;

    [SerializeField]  public TextField CreateGameInput;

    [SerializeField]  public TextField JoinGameInput;

    private void Awake()
    {
        Photon.Pun.PhotonNetwork.ConnectUsingSettings();
    }
    // Start is called before the first frame update
    void Start()
    {
        UsernameMenu.SetActive(true);
        
    }


    // Update is called once per frame
    private void OnConnectedToMaster()
    {
        Photon.Pun.PhotonNetwork.JoinLobby(Photon.Realtime.TypedLobby.Default);
        Debug.Log("Connected");
    }
    public void SetUsername()
    {
        UsernameMenu.SetActive(false);
        Photon.Pun.PhotonNetwork.LocalPlayer.NickName = UsernameInput.text;
    }
}
