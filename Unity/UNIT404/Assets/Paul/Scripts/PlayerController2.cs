using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashCooldown = 10f;
    [SerializeField] private float dashIntensity;
    private float nextDash;
    private Rigidbody myRigidBody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Camera mainCamera;
    public gunController theGun;
    Animator animator;
    //PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();

        //view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (view.IsMine)
        //{
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        float VelocityZ = Vector3.Dot(moveVelocity.normalized, transform.forward);
        float VelocityX = Vector3.Dot(moveVelocity.normalized, transform.right);

        animator.SetFloat("VelocityZ", VelocityZ, 0.005f, Time.deltaTime);
        animator.SetFloat("VelocityX", VelocityX, 0.005f, Time.deltaTime);


        HandleShootInput2();
        
        //}
    }
    private void FixedUpdate2()
    {
        myRigidBody.velocity = moveVelocity;
        if (Input.GetKey(KeyCode.Space) && Time.time > nextDash)
        {
            nextDash = Time.time + dashCooldown;
            Dash2();
        }
    }

    void HandleShootInput2()
    {

        if (Input.GetButton("Fire1"))
        {
            //gunController.Instance.Shoot();

        }

    }

    void Dash2()
    {
        myRigidBody.AddForce(myRigidBody.velocity * dashIntensity, ForceMode.Impulse);
    }
}