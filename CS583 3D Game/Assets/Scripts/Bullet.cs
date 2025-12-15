using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public float damage = 25f;
    public float lifeTime = 2f;

    public GameObject hitEffect;
    public float effectLife;

    void Update()
    {
        // move bullet forward every frame
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (lifeTime <= 0f)
        {
            DestroyBullet();
        } 
        else
        {
            lifeTime = Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<ITakeDamage>().TakeDamage(damage);
            DestroyBullet();
        }

        if (other.CompareTag("Wall"))
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, effectLife);

        Destroy(gameObject);
    }
}
