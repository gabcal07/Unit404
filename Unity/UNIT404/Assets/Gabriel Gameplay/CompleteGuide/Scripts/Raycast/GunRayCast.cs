using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunRayCast : MonoBehaviour
{
    public bool isHeavy;
    public float explosionDamage;
    public float explosionRadius;
    public float damage = 10f;
    public float range = 100f;
    public float firerate;
    public float impactForce;

    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1;
    private bool isReloading = false;

    public GameObject firingPoint;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    PhotonView view;

    private float nextTimeToFire = 0f;

    public bool UsingPhoton;

    private void Start()
    {
       
        view = this.gameObject.GetComponentInParent<PhotonView>();
        if (view.IsMine)
        {
            currentAmmo = maxAmmo;
        }
        else
        {
            this.gameObject.GetComponent<GunRayCast>().enabled = false;
        }

    }

    private void OnEnable()
    {
        isReloading = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        RaycastHit hit;

        if (Physics.Raycast(firingPoint.transform.position, firingPoint.transform.forward, out hit, range))
            Gizmos.DrawWireSphere(hit.point, explosionRadius);
    }
    void Update()
    {
        if (UsingPhoton)
        {
            if (view!=null && view.IsMine)
            {
                if (isReloading) { return; }
                  

                if (currentAmmo <= 0)
                {
                    StartCoroutine(Reload());
                    return;
                }
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / firerate;
                    Shoot();
                }
            }
        }
        else
        {
            if (isReloading)
            { 
                return;

            } 
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / firerate;
                Shoot();
            }
        }
        
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        currentAmmo--;
        muzzleFlash.Play(); 
        RaycastHit hit;

        if(Physics.Raycast(firingPoint.transform.position, firingPoint.transform.forward, out hit, range))
        {
            if (isHeavy)
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    Collider[] damaged = Physics.OverlapSphere(hit.point, explosionRadius);
                    foreach (Collider collider in damaged)
                    {
                        Target target1 = collider.transform.GetComponent<Target>();
                        if(target1 != null)
                        {
                            target1.TakeDamage(explosionDamage);
                        }
                    }
                }
            }
            else
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);


            
            
        }

    }
}
