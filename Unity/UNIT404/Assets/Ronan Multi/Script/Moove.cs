using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Photon.Pun;

public class Moove : MonoBehaviour
{
    public int speed;
    PhotonView view;
    public int flyspeed;
    public const float maxDashTime = 1.0f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 6;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 moveDirection = new Vector3(x, 0, z);
            gameObject.transform.Translate(moveDirection * speed * Time.deltaTime);
            if (Input.GetButton("Jump"))
            {
                moveDirection.y += flyspeed;
                gameObject.transform.Translate((moveDirection * speed * Time.deltaTime));
           
            } 
            
            if (Input.GetButton("Fire3")) 
            {
                gameObject.transform.Translate(moveDirection * 2 * speed * Time.deltaTime);

            }
            if (Input.GetButtonDown("Fire2")) //Right mouse button
            {
                currentDashTime = 0;
            }
            if (currentDashTime < maxDashTime)
            {
                moveDirection*= dashDistance;
                currentDashTime += dashStoppingSpeed;
            }
            else
            {
                moveDirection = Vector3.zero;
            }
            gameObject.transform.Translate(moveDirection * Time.deltaTime * dashSpeed);


        }
    }
}
