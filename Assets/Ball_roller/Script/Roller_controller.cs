using UnityEngine;
using UnityEngine.InputSystem;

public class Roller_controller : MonoBehaviour
{
    [Range(1, 50)][SerializeField] float speed = 3.0f;
    [Range(1, 50)][SerializeField] float jumpForce = 3.0f;

    [SerializeField] Transform view;

    [SerializeField] float rayLength = 1;

    [SerializeField] LayerMask groundLayerMask;

    private Rigidbody rb;
    Vector2 movementInput = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new(movementInput.x, 0, movementInput.y);
        movement = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up) * movement;
        rb.AddForce(movement * speed);
    }

    public void OnMove(InputValue input)
    {
        movementInput = input.Get<Vector2>();
    }

    public void OnJump()
    {
        if (onGround())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

        }
    }

    bool onGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayerMask);        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Vector3.down * rayLength);
    }
}
