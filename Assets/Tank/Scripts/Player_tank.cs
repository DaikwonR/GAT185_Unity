using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player_tank : MonoBehaviour, I_damageable
{
    [Range(1, 360)] [SerializeField] float maxTorque = 25;
    [Range(50, 1000)] [SerializeField] float maxForce = 75;
    [SerializeField] GameObject rocket;
    [SerializeField] Transform barrel;
    public int ammo = 10;
    [SerializeField] TMP_Text ammoText;

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

        if (Input.GetButtonDown("Fire1") && ammo > 0)
        {
            Instantiate(rocket, barrel.position, barrel.rotation);
        }

        ammoText.text = "Ammo: " + ammo.ToString();
    }


    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * force);
        rb.AddRelativeTorque(Vector3.up * torque);
    }

    public void ApplyDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
