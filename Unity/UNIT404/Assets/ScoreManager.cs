using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreManager : MonoBehaviour
{


    public float Score;
    public int Round;
    public float Kill;
    public TMP_Text score;
    public TMP_Text round;
    public TMP_Text kill;
    public TMP_Text FinalMessage;
    public string[] CreditTitre = { "Jouabilit� ", "Animations", "Intelligence Artificielle", "Cr�ation des niveaux", "Musiques et Bruitages", "Interfaces Utilisateurs", "Site Internet et communication" };
    public string[] CreditPerson = { "Grabiel Calvente", "Paul Rousseau", "Zacharie Rhode", "Ronan Le Goff", "Gabriel Calvente", "Paul Rousseau \n Ronan Le goff", "Zacharie Rhode" };
    // Start is called before the first frame update
    void Start()
    {
        Score = PlayerPrefs.GetFloat("Score");
        Debug.Log("Score: "+Score);
        score.text = "Score: " + Score.ToString();
        Round = PlayerPrefs.GetInt("Round");
        Debug.Log("Round: "+Round);
        round.text = "Manches surv�cues: " + Round.ToString();

        Kill = PlayerPrefs.GetFloat("Kill");
        Debug.Log("Kill: " + Kill);
        kill.text = "Ennemis tu�s: " + Kill.ToString();
        if (Round >= 5)
        {
            FinalMessage.text = "Bravo � vous soldat(s)! L'humanit� vous acclamera!";
        }
        else
        {
            FinalMessage.text = "Vous avez lamentablement �chou�...";
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveR()
    {
        PhotonNetwork.LeaveRoom();
    }
}
