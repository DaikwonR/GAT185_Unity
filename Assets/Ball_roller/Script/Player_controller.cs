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

    Vector2 movementInput = Vector2.zero;
    [Range(1, 100)] Vector3 velocity = Vector3.zero;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        jumpAction = InputSystem.actions.FindAction("Jump");
        jumpAction.performed += OnJump;
        jumpAction.canceled += OnJump;

        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        Vector3 movement = new(movementInput.x, 0, movementInput.y);
        movement = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up) * movement;

        float moveModifier = (controller.isGrounded) ? 1 : 0;

        velocity.x = movement.x * data.speed;
        velocity.z = movement.z * data.speed;

        if (movement.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * data.turnRate);
            // transform.forward = movement;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // configure animator

        Vector3 v = velocity;
        v.y = 0;
        animator.SetFloat("Speed", v.magnitude);
        animator.SetFloat("Y-Velocity", controller.velocity.y);
        animator.SetBool("On-Ground", controller.isGrounded);
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
        }
    }
}
