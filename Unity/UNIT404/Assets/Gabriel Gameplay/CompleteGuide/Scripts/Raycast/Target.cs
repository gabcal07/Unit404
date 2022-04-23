using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float amount)
    {

        health -= amount;
        if (health <= 0f)
            Die();
    }

    void Die()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
