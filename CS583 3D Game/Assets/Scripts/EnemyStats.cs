using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private Renderer rend;

    [Header("Stats")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float damage = 10f;
    public float speed = 3f;
    public float resistance = 0f;
    public int pointValue = 10;


    void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.green;
        }
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

        
        if (rend != null)
        {
            float t = Mathf.Clamp01(currentHealth / maxHealth);
            rend.material.color = Color.Lerp(Color.red, Color.green, t);
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

}
