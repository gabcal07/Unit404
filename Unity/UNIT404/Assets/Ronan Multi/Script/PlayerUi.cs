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
            }
            if (NumEnnemiesText != null)
            {
                NumEnnemiesText.text = "Number of Enemies: \n " + (GameObject.Find("GameManager").transform.childCount.ToString());
            }

        }
        if (target == null)
            {
                Destroy(this.gameObject);
                return;
            }
       
    }

    #endregion
}