using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputSystem : MonoBehaviour
{
    private Rigidbody capsuleRigidBody;
    private PlayerInput playerInput;
    private PlayerControls playerControls;

    private void Awake()
    {
        capsuleRigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        PlayerControls playerControls = new PlayerControls();
        playerControls.Player.Enable();
        playerControls.Player.Jump.performed += Jump;
        playerControls.Player.Movement.performed += Movement_performed;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        float speed = 5f;
        capsuleRigidBody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    public void Movement_performed(InputAction.CallbackContext c)
    {
        Debug.Log("Moves" + c);
        Vector2 inputVector = c.ReadValue<Vector2>(); //input reading
        float speed = 5f;
        capsuleRigidBody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force); //vector 3 cuz the player is in 3D
    }

    public void Jump(InputAction.CallbackContext c)
    {
        if (c.performed)
        {
            Debug.Log("Jump!" + c.phase);
            capsuleRigidBody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
