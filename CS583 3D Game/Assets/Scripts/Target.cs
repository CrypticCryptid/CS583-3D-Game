using UnityEngine;

public class Target : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Renderer rend;

    void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.green;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        // Visual feedback: green → red as health goes down
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

    void Die()
    {
        Destroy(gameObject); // JUST destroys itself
    }
}
