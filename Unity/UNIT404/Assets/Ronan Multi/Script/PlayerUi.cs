using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerUi : MonoBehaviour
{
    #region Private Fields


    [Tooltip("UI Text to display Player's Name")]
    [SerializeField]
    public TMP_Text playerNameText;
    public  GameObject target;
    [Tooltip("UI Slider to display Player's Health")]
    [SerializeField]
    private Slider playerHealthSlider;
    public TMP_Text NumEnnemiesText;
    public TMP_Text hp;
    public TMP_InputField namechanger;
    public TMP_Text round;
    public GameObject weapon;
    public TMP_Text Cmun;
    public TMP_Text Mmun;
    public TMP_Text equiped;




    #endregion


    #region MonoBehaviour Callbacks

    void Awake()
    {
        playerNameText.text = target.GetComponent<PhotonView>().Owner.NickName;
    }
    void Update()
    {
        if (target != null)
        { // Reflect the Player Health
            if (playerHealthSlider != null)
            {
                playerHealthSlider.value = target.GetComponentInParent<PManager>().Health;
                hp.text = "PV: " + target.GetComponentInParent<PManager>().Health.ToString() + "/200";
            }
            if (NumEnnemiesText != null)
            {
                NumEnnemiesText.text = "Number of Enemies: \n " + (GameObject.Find("GameManager").transform.childCount.ToString());
            }
            if (PhotonNetwork.LocalPlayer.NickName == null || PhotonNetwork.LocalPlayer.NickName == "")
            {
                playerNameText.text = "Select Nickname in pause menu";
            }
            else
            {
                playerNameText.text = PhotonNetwork.LocalPlayer.NickName;

            }
            if (round != null)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().BossSpawned)
                {
                    round.text = "ENNEMI DANGEREUX";
                }
                else 
                {
                    round.text = "Manche: " + GameObject.Find("GameManager").GetComponent<GameManager>().round.ToString(); 
                }
            }

            if (weapon != null)
            {
                string amo = weapon.GetComponentInChildren<GunRayCast>().currentAmmo.ToString();
                string maxAmo= weapon.GetComponentInChildren<GunRayCast>().maxAmmo.ToString();
                Cmun.text = amo;
                Mmun.text = maxAmo;
                
            } 
        }


            if (target == null)
            {
                Destroy(this.gameObject);
                return;
            }
            
       
    }

    public void changeName()
    {

        PhotonNetwork.LocalPlayer.NickName = namechanger.text;
    }

    #endregion
}