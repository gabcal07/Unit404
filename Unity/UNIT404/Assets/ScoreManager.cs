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
    public string[] CreditTitre = { "Jouabilité ", "Animations", "Intelligence Artificielle", "Création des niveaux", "Musiques et Bruitages", "Interfaces Utilisateurs", "Site Internet et communication" };
    public string[] CreditPerson = { "Grabiel Calvente", "Paul Rousseau", "Zacharie Rhode", "Ronan Le Goff", "Gabriel Calvente", "Paul Rousseau \n Ronan Le goff", "Zacharie Rhode" };
    // Start is called before the first frame update
    void Start()
    {
        Score = PlayerPrefs.GetFloat("Score");
        Debug.Log("Score: "+Score);
        score.text = "Score: " + Score.ToString();
        Round = PlayerPrefs.GetInt("Round");
        Debug.Log("Round: "+Round);
        round.text = "Manches survécues: " + Round.ToString();

        Kill = PlayerPrefs.GetFloat("Kill");
        Debug.Log("Kill: " + Kill);
        kill.text = "Ennemis tués: " + Kill.ToString();
        if (Round >= 5)
        {
            FinalMessage.text = "Bravo à vous soldat(s)! L'humanité vous acclamera!";
        }
        else
        {
            FinalMessage.text = "Vous avez lamentablement échoué...";
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
