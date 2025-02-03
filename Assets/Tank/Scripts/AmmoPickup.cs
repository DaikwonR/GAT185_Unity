using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Range(1, 30)][SerializeField] int ammoCount = 5;
    [SerializeField] GameObject pickupFX;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Player_tank component))
            {
                component.ammo += ammoCount;
                Destroy(gameObject);
                if (pickupFX != null)
                {
                    Instantiate(pickupFX, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
