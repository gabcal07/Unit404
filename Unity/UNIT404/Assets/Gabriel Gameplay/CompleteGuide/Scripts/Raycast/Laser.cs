using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laser;
    public float gunrange;
    public float LaserDuration = 0.05f;
    LineRenderer laserLine;
    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        laserLine.SetPosition(0, laser.position);
        RaycastHit hit;
        if (Physics.Raycast(laser.position, laser.forward, out hit, gunrange))
        {
            laserLine.SetPosition(1, hit.point);
        }
        else
        {
            laserLine.SetPosition(1, laser.position + laser.forward * gunrange);
        }
    }
}
