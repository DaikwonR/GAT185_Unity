using UnityEngine;

public class Tank : MonoBehaviour
{
    [Range(1, 360)] [SerializeField] float turnRate = 15;
    [Range(1, 50)] [SerializeField] float maxSpeed = 1;
    [SerializeField] GameObject rocket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal");
        float speed = Input.GetAxis("Vertical");

        transform.rotation *= Quaternion.AngleAxis(rotation * turnRate * Time.deltaTime, Vector3.up);
        //transform.position += transform.rotation * (Vector3.forward * speed * maxSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * maxSpeed * Time.deltaTime, Space.Self);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(rocket, transform.position + Vector3.up, transform.rotation);
        }
    }
}
