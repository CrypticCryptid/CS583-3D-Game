using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public float damage = 25f;
    public float lifeTime = 2f;

    void Start()
    {
        // destroy bullet after some time so it doesn't live forever
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // move bullet forward every frame
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if we hit a target
        Target target = other.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(damage);
        }

        // destroy bullet on hit — important!
        Destroy(gameObject);
    }
}