using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class enemyHealthManager : MonoBehaviour
{
    public int health;
    [SerializeField] private int currentHealth;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = this.gameObject.GetComponent<PhotonView>();
        currentHealth = health;    
    }

    // Update is called once per frame
    void Update()
    {
        Die(this.gameObject);

 
    }
    public void Die(GameObject me)
    {
        if (view.IsMine)
        {
            if (currentHealth <= 0)
            {
                PhotonNetwork.Destroy(me);
            }

        }

    }
    public void HurtEnemy(int damage)
    {
        currentHealth -= damage;
    }
}
