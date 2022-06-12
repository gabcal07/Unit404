using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckDeath : MonoBehaviour
{
    public GameObject manager;
    GameManager managerDatas;
    public float Score;
    public int MancheSurvécues;
    public float ennemisTués;
    public bool InCalcul = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        managerDatas = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AllMyFriendAreDead() && !InCalcul)
        {
            Debug.Log("Entrer dans update");
            InCalcul = true;
            Score = 0;
            MancheSurvécues = 0;
            ennemisTués = 0;
            bool desert = managerDatas.Desert;
            if (desert)
            {
                MancheSurvécues = 5 + managerDatas.round - 1;
                ennemisTués = 330;
                for (int i = 0; i < MancheSurvécues; i++)
                {
                    ennemisTués += managerDatas.SpawnDatas(i)[0];
                }

            }
            else
            {
                MancheSurvécues = managerDatas.round - 1;
                for (int i = 0; i < MancheSurvécues; i++)
                {
                    ennemisTués += managerDatas.eachRound[i, 0];
                }
                ennemisTués +=  managerDatas.AlreadySpawned-manager.transform.childCount;
            }
            Score = (MancheSurvécues+1) * ennemisTués * Mathf.Exp(MancheSurvécues);
            Score = Mathf.Round(Score);
            ennemisTués = Mathf.Round(ennemisTués);
            PlayerPrefs.SetFloat("Kill", ennemisTués);
            PlayerPrefs.SetFloat("Score", Score);
            PlayerPrefs.SetInt("Round", MancheSurvécues);
            PhotonNetwork.LoadLevel("ScoreScreen");
            //foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            //{
              //  PhotonNetwork.CloseConnection(p);
            //}
        }
        
    }

    public bool AllMyFriendAreDead() 
    {
        if (manager.GetComponent<GameManager>().playerList.Count != 0)
        {
            foreach (GameObject p in manager.GetComponent<GameManager>().playerList)
            {
                //Debug.Log("Is dead:" + p.GetComponentInChildren<Leave>().dead);
                if (!p.GetComponentInChildren<Leave>().dead)
                {
                    //Debug.Log("CheackDeath: return false");
                    return false;

                }
            }
            //Debug.Log("CheackDeath: return true");
           // Debug.Log("InCalcul: "+ InCalcul);
            return true;
        }
        return false;

    }

    [PunRPC]
    public void KickPlayers()
    {
        PhotonNetwork.LeaveRoom();
    }
}
