using UnityEngine;

public class Damage_source : MonoBehaviour
{
    [Range(1, 25)][SerializeField] float damage = 1;
    [Range(1, 25)][SerializeField] float stackDamage = 1;
    [SerializeField] bool destroyOnHit = true;

    // objects are typically not triggers
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out I_damageable _damageable))
        {
            _damageable.ApplyDamage(damage);
        }

        if (destroyOnHit)
        {
            Destroy(gameObject);
        }
    }
}
