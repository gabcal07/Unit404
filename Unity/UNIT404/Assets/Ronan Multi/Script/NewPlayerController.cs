using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashCooldown = 10f;
    [SerializeField] private float dashIntensity;
    private float nextDash;
    private Rigidbody myRigidBody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    public Camera mainCamera;
    public gunController theGun;
    PhotonView view;
    Animator animator;
    private bool isColliding;
    private int collisions = 0;
    public GameObject PlayerM;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        //mainCamera = FindObjectOfType<Camera>();
        view = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (view != null && view.IsMine)
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput * moveSpeed;
            if (mainCamera != null)
            {
                Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayLength;

                if (groundPlane.Raycast(cameraRay, out rayLength))
                {
                    Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                    Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                    transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                }

                HandleShootInput();
            }

        }
        else
        {
            return;
        }
      
    }
    void OnCollisionEnter(Collision collision)
    {
        if (view != null && view.IsMine)
        {
            collisions++;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (view != null && view.IsMine)
        {
            collisions--;
        }
    }

    private void FixedUpdate()
    {
        myRigidBody.velocity = moveVelocity;
        if (view != null && view.IsMine)
        {
            float VelocityZ = Vector3.Dot(moveVelocity.normalized, transform.forward);
            float VelocityX = Vector3.Dot(moveVelocity.normalized, transform.right);

            animator.SetFloat("VelocityZ", VelocityZ, 0.005f, Time.deltaTime);
            animator.SetFloat("VelocityX", VelocityX, 0.005f, Time.deltaTime);
            if (Input.GetKey(KeyCode.Space) && Time.time > nextDash)
            {
                nextDash = Time.time + dashCooldown;
                Dash();
            }
        }


        if (view != null && view.IsMine)
        {
            //Debug.Log(collisions);
            if (collisions == 0) myRigidBody.AddForce(new Vector3(0, -75, 0), ForceMode.Force);
        }
    }


    void HandleShootInput()
    {

        if (Input.GetButton("Fire1"))
        {
            //gunController.Instance.Shoot();

        }

    }

    void Dash()
    {
        PlayerM.GetComponent<AudioManager>().dashing();
        myRigidBody.AddForce(myRigidBody.velocity * dashIntensity, ForceMode.Impulse);
    }
}