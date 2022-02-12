using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMovement : MonoBehaviour
{
    // variables to store character animator component
    Animator animator;

    // variables to store optimized setter/getter parameter IDs
    int isWalkingHash;
    int isRunningHash;

    // variables to store the instance of the PlayerInput
    PlayerInput input;

    // variables to store player input values
    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        input = new PlayerInput();
        input.CharacterControls.Movements.performed += ctx => currentMovement = ctx.ReadValue<Vector2>();
        input.CharacterControls.Walk.performed += ctx => runPressed = ctx.ReadValueAsButton();
    }
    // Start is called before the first frame update
    void Start()
    {
        // set the animator reference
        animator = GetComponent<Animator>();

        // set the ID references
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void handleMovement()
    {
        // get parameter values from animator
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
    }

    private void OnEnable()
    {
        // enable the character controls action map
        input.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        // disable the character controls action map
        input.CharacterControls.Disable();
    }
}
