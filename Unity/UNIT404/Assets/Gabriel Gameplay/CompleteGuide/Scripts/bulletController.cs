using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    private Vector3 firingPoint;
    [SerializeField] float projectileSpeed;
    public int damageToGive;
    [SerializeField] float maxProjectileDistance;
    // Start is called before the first frame update
    void Start()
    {
        firingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveProjectile();
    }

    void moveProjectile()
    {
        if (Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag is "enemy")
        {
            collision.gameObject.GetComponent<enemyHealthManager>().HurtEnemy(damageToGive);
            Destroy(gameObject);
        }
    }
}
