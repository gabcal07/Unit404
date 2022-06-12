using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckDeath : MonoBehaviour
{
    public GameObject manager;
    GameManager managerDatas;
    public float Score;
    public int MancheSurv�cues;
    public float ennemisTu�s;
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
            MancheSurv�cues = 0;
            ennemisTu�s = 0;
            bool desert = managerDatas.Desert;
            if (desert)
            {
                MancheSurv�cues = 5 + managerDatas.round - 1;
                ennemisTu�s = 330;
                for (int i = 0; i < MancheSurv�cues; i++)
                {
                    ennemisTu�s += managerDatas.SpawnDatas(i)[0];
                }

            }
            else
            {
                MancheSurv�cues = managerDatas.round - 1;
                for (int i = 0; i < MancheSurv�cues; i++)
                {
                    ennemisTu�s += managerDatas.eachRound[i, 0];
                }
                ennemisTu�s +=  managerDatas.AlreadySpawned-manager.transform.childCount;
            }
            Score = (MancheSurv�cues+1) * ennemisTu�s * Mathf.Exp(MancheSurv�cues);
            Score = Mathf.Round(Score);
            ennemisTu�s = Mathf.Round(ennemisTu�s);
            PlayerPrefs.SetFloat("Kill", ennemisTu�s);
            PlayerPrefs.SetFloat("Score", Score);
            PlayerPrefs.SetInt("Round", MancheSurv�cues);
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
