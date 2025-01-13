using UnityEngine;

public class player_tank : MonoBehaviour
{
    [Range(1, 360)] [SerializeField] float maxTorque = 25;
    [Range(50, 1000)] [SerializeField] float maxForce = 75;
    [SerializeField] GameObject rocket;
    [SerializeField] Transform barrel;

    float torque;
    float force;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        torque = Input.GetAxis("Horizontal") * maxTorque;
        force = Input.GetAxis("Vertical") * maxForce;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(rocket, barrel.position, barrel.rotation);
        }
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * force);
        rb.AddRelativeTorque(Vector3.up * torque);
    }
}
