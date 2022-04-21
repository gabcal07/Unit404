using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Transform FiringPoint;
    public ParticleSystem Flames;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Flames.Play();
        Collider[] damaged;
        damaged = Physics.OverlapCapsule(transform.position, transform.position + new Vector3(transform.position.x + range, transform.position.y, transform.position.z), 30);
        foreach (Collider touched in damaged)
        {
            if (touched.tag == "enemy")
            {
                Target target = touched.GetComponent<Target>();
                if (target != null)
                    target.TakeDamage(damage);
            }
        }
    }
}
