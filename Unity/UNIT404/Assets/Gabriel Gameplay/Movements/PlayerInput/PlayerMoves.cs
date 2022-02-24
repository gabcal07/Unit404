using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    public bool jumpPressed;
    public float speed = 0.75f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rigidbodyComponent;
    [SerializeField] private Transform groundCheckTansform = null;

    private void Start()
    {
        rigidbodyComponent = GetComponent < Rigidbody > ();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
       
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 10f, rigidbodyComponent.velocity.y, verticalInput * 10f);

        if (Physics.OverlapSphere(groundCheckTansform.position, 0.1f).Length == 1)
        {
            return;
        }
        if (jumpPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 10, ForceMode.VelocityChange);
            jumpPressed = false;
        }      
    }

}
