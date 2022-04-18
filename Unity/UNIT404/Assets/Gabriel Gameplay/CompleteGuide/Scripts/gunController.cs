using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class gunController : MonoBehaviour
{
    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float firingSpeed;
    public static gunController Instance;
    private float lastTimeShot = 0f;
    PhotonView view;
   
    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            Instance = GetComponent<gunController>();
        }
     
    }

    // Update is called once per frame

    public void Shoot()
    {
        if (lastTimeShot + firingSpeed <= Time.time)
        {
            lastTimeShot = Time.time;
            /*PhotonNetwork.*/Instantiate(projectilePrefab/*.name*/, firingPoint.position, firingPoint.rotation);
        }
    }
}
