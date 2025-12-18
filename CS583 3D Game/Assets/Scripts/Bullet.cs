using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerStats Stats;

    public float speed = 50f;
    public float damage = 25f;
    public float lifeTime = 2f;

    public GameObject hitEffect;
    public float effectLife;

    void Update()
    {
        // move bullet forward along its local Z
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // lifetime countdown
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Look up the hierarchy for something that can take damage
        ITakeDamage damageReceiver = other.GetComponentInParent<ITakeDamage>();

        if (damageReceiver != null)
        {
            damageReceiver.TakeDamage(damage);
            DestroyBullet();
            return;
        }

        // Still destroy on walls etc.
        if (other.CompareTag("Wall"))
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        if (hitEffect != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, effectLife);
        }

        Destroy(gameObject);
    }
}
