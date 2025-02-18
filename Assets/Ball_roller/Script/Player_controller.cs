using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player_controller : MonoBehaviour
{
    [SerializeField] Player_data data;
    [SerializeField] Transform view;
    [SerializeField] Animator animator;

    CharacterController controller;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction sprintAction;
    InputAction kickAction;
    InputAction punchAction;

    bool isSprinting = false;
    bool isAttacking = false;

    Vector2 movementInput = Vector2.zero;
    [Range(1, 100)] Vector3 velocity = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        jumpAction = InputSystem.actions.FindAction("Jump");
        jumpAction.performed += OnJump;
        jumpAction.canceled += OnJump;
        
        sprintAction = InputSystem.actions.FindAction("Sprint");
        sprintAction.performed += OnSprint;
        sprintAction.canceled += OnSprint;
        
        kickAction = InputSystem.actions.FindAction("Kick");
        kickAction.performed += OnKick;
        kickAction.canceled += OnKick;
        
        punchAction = InputSystem.actions.FindAction("Punch");
        punchAction.performed += OnPunch;
        punchAction.canceled += OnPunch;
    }

    void Update()
    {
        // Check if the player is on ground
        bool onGround = controller.isGrounded;// || (velocity.y < 0 && PredictGroundContact());

        // Reset vertical velocity when grounded to prevent accumulating downward force
        if (onGround && velocity.y < 0)
        {
            velocity.y = -1; // Small downward force to keep player grounded
        }

        // Convert movement input into a world-space direction based on the player's view rotation
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);
        movement = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up) * movement;

        // Initialize acceleration vector for movement calculations
        Vector3 acceleration = Vector3.zero;
        acceleration.x = movement.x * data.acceleration;
        acceleration.z = movement.z * data.acceleration;

        // Reduce acceleration while in the air for smoother movement control
        if (!onGround) acceleration *= 0.1f;

        // Extract horizontal velocity (ignoring vertical movement)
        Vector3 vXZ = new Vector3(velocity.x, 0, velocity.z);

        // Apply acceleration to velocity while limiting max speed
        vXZ += acceleration * Time.deltaTime;
        vXZ = Vector3.ClampMagnitude(vXZ, (isSprinting) ? data.sprint : data.walk);

        // Assign updated velocity values
        velocity.x = vXZ.x;
        velocity.z = vXZ.z;

        // Apply drag to slow the player down when there is no input or when airborne
        if (movement.sqrMagnitude <= 0 || !onGround)
        {
            float drag = (onGround) ? 10 : 4;
            velocity.x = Mathf.MoveTowards(velocity.x, 0, drag * Time.deltaTime);
            velocity.z = Mathf.MoveTowards(velocity.z, 0, drag * Time.deltaTime);
        }

        // Smoothly rotate the player towards the movement direction
        if (movement.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * data.turnRate);
        }

        // Apply gravity
        velocity.y += data.gravity * Time.deltaTime;

        // Move the character using the CharacterController component
        controller.Move(velocity * Time.deltaTime);

        // Update animator parameters for movement animations
        animator.SetFloat("Speed", new Vector3(velocity.x, 0, velocity.z).magnitude);
        animator.SetFloat("AirSpeed", controller.velocity.y);
        animator.SetBool("OnGround", onGround);
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        movementInput = callbackContext.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2 * data.gravity * data.jumpHeight);
            animator.SetTrigger("Jump");
        }
    }

    public void OnSprint(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed) isSprinting = true;
        else if (callbackContext.phase == InputActionPhase.Canceled) isSprinting = false;
    }
    
    public void OnPunch(InputAction.CallbackContext callbackContext)
    {

        if (callbackContext.phase == InputActionPhase.Performed)
        {
            isAttacking = true;
            animator.SetTrigger("Punch");
        }
        else if (callbackContext.phase == InputActionPhase.Canceled) isAttacking = false;
    }
    
    public void OnKick(InputAction.CallbackContext callbackContext)
    {

        if (callbackContext.phase == InputActionPhase.Performed)
        {
            isAttacking = true;
            animator.SetTrigger("Kick");
        }
        else if (callbackContext.phase == InputActionPhase.Canceled) isAttacking = false;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rb = hit.collider.attachedRigidbody;

        if (rb == null || rb.isKinematic || hit.moveDirection.y < -0.3) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        rb.linearVelocity = pushDir * data.pushForce;
    }
}
