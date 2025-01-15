using UnityEngine;

public class Destructable : MonoBehaviour, I_damageable
{
    [Range(10, 100)][SerializeField] float health = 20.0f;
    [SerializeField] GameObject destroyFX;

    bool destroyed = false;

    public void ApplyDamage(float damage)
    {
        if (destroyed) return;

        health -= damage;
        if (health <= 0)
        {
            destroyed = true;
            if (destroyFX != null) Instantiate(destroyFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
