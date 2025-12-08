using UnityEngine;

public class EnemyStats : Stats, ITakeDamage
{
    [Header("Stats")]
    public int pointValue;

    protected override void Start()
    {
        //assign stuff
        maxHealth = 100f;
        damage = 10f;
        speed = 1f;
        resistance = 0f;
        pointValue = 10;

        currentHealth = maxHealth;
    }

    public override void Die()
    {
        // Handle enemy death
        Destroy(gameObject);
    }
}