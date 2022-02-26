using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float firingSpeed;
    public static PlayerGun Instance;
    private float lastTimeShot = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = GetComponent<PlayerGun>();
    }

    // Update is called once per frame

    public void Shoot()
    {
        if (lastTimeShot + firingSpeed <= Time.time)
        {
            lastTimeShot = Time.time;
            Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        }
    }
}
