using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_controller : MonoBehaviour
{
    [SerializeField] Transform target;
    [Range(1, 10)][SerializeField] float distance;
    [Range(1, 10)][SerializeField] float sensitivity = 1;

    InputAction lookAction;
    Vector2 lookInput;
    Vector3 rotation = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        lookAction.performed += Look;
        lookAction.canceled += Look;

        Quaternion qrotation = Quaternion.LookRotation(target.position - transform.position);
        rotation.x = qrotation.eulerAngles.x;
        rotation.y = qrotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.x += lookInput.y;
        rotation.y += lookInput.x;

        rotation *= sensitivity;

        rotation.x = Mathf.Clamp(rotation.x, 20, 80);

        Quaternion qrotation = Quaternion.Euler(rotation);

        transform.position = target.position + qrotation * (Vector3.back * distance);
        transform.rotation = qrotation;
    }

    void Look(InputAction.CallbackContext callbackContext)
    {
        lookInput = callbackContext.ReadValue<Vector2>();

        
    }
}
