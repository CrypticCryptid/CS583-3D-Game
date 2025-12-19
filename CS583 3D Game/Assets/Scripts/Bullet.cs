using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public float damage;
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
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<ITakeDamage>().TakeDamage(damage);
            DestroyBullet();
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
