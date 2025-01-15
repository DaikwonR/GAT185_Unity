using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Range(1, 50)]public float force = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force, ForceMode.VelocityChange);
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
}
