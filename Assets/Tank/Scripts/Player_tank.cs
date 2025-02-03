using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_tank : MonoBehaviour
{
    [Range(1, 360)] [SerializeField] float maxTorque = 25;
    [Range(50, 1000)] [SerializeField] float maxForce = 75;
    [SerializeField] GameObject rocket;
    [SerializeField] Transform barrel;

    [SerializeField] public int ammo = 10;
    [SerializeField] TMP_Text ammoText;
    
    [SerializeField] public int points = 0;
    [SerializeField] TMP_Text pointText;

    [SerializeField] Slider healthSlider;

    float torque;
    float force;

    Rigidbody rb;

    Destructable destructable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        destructable = GetComponent<Destructable>();
    }

    // Update is called once per frame
    void Update()
    {
        torque = Input.GetAxis("Horizontal") * maxTorque;
        force = Input.GetAxis("Vertical") * maxForce;

        if (Input.GetButtonDown("Fire1") && ammo > 0)
        {
            ammo--;
            Instantiate(rocket, barrel.position, barrel.rotation);
        }

        ammoText.text = "Ammo: " + ammo.ToString();

        pointText.text = "Points: " + points.ToString();

        healthSlider.value = destructable.Health;
        if (destructable.Health <= 0)
        {
            Game_manager.Instance.SetGameOver();
        }

    }


    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * force);
        rb.AddRelativeTorque(Vector2.up * torque);
    }

}
