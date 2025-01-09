using JetBrains.Annotations;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Range(1, 10)] public float speed = 2;
    public GameObject prefab;
    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        Vector3 velocity = Vector3.zero;
        velocity.x = Input.GetAxis("Horizontal");
        velocity.z = Input.GetAxis("Vertical");
        transform.position += velocity * speed * Time.deltaTime;
        
        // Create prefab
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(prefab, transform.position + Vector3.up, Quaternion.identity);
        }
        #region
        //if (Input.GetButton("Fire1"))
        //{
        //    position.y += 25.0f * Time.deltaTime;

        //}
        //if (position.y > 100)
        //{
        //    position.y -= 25.0f;
        //    transform.position = position;
        //}
        //if (position.y < 0)
        //{
        //    position.y += 25.0f;
        //    transform.position = position;
        //}
        #endregion
    }
}
