using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    public bool jumpPressed;
    public float speed = 8f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rigidbodyComponent;
    [SerializeField] private Transform groundCheckTansform = null;
    [SerializeField] private float movementSpeed;

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

        //HandleMovementInput(); 

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput * speed, rigidbodyComponent.velocity.y, verticalInput * speed);

        if (Physics.OverlapSphere(groundCheckTansform.position, 0.1f).Length == 1)
        {
            return;
        }
        if (jumpPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpPressed = false;
        }
    }

    private void HandleMovementInput()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        Vector3 _movement = new Vector3(_horizontal, rigidbodyComponent.velocity.y, _vertical);
        transform.Translate(_movement * movementSpeed * Time.deltaTime, Space.World);
    }
}
