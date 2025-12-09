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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.ScoreUpdate("Player");
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.Reset();
        }
    }
    public void TakeDamage(float amount)
    {
        Debug.Log("Target Hit");
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
        GameManager.Instance.ScoreUpdate("Enemy");
        Destroy(gameObject); // JUST destroys itself
    }
}
