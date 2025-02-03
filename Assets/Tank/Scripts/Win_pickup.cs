using UnityEngine;

public class Win_pickup : MonoBehaviour
{
    [Range(1, 30)][SerializeField] int pointCount = 30;
    [SerializeField] GameObject pickupFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Player_tank component))
            {
                component.points += pointCount;
                Destroy(gameObject);
                if (pickupFX != null || pickupFX == null)
                {
                    Instantiate(pickupFX, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
