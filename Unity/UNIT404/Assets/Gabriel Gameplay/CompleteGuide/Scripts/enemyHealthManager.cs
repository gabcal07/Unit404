using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthManager : MonoBehaviour
{
    public int health;
    [SerializeField] private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;    
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HurtEnemy(int damage)
    {
        currentHealth -= damage;
    }
}
