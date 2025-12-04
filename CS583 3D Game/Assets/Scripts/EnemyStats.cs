using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [Header("Stats")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float damage = 10f;
    public float speed = 3f;
    public float resistance = 0f;
    public int pointValue = 10;


    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Die()
    {
        // Handle enemy death
        Destroy(gameObject);
    }
    public void takeDamage(float amount)
    {
        float effectiveDamage = amount * (1 - resistance);
        currentHealth -= effectiveDamage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

}
