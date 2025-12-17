using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats, ITakeDamage
{
    [Header("Stats")]
    public int pointValue;

    private WaveManager manager;
    private Door door;

    public float invulnerabilityDuration = 0.5f;
    public float blinkInterval = 0.075f;
    private bool isInvulnerable = false;
    public SpriteRenderer spriteRenderer;

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
        manager.RemoveEnemy(gameObject);

        if(door != null)
            door.CheckToClose();

        Destroy(gameObject);
    }

    public void SetManager(WaveManager obj)
    {
        manager = obj;
    }

    public void SetDoor(Door obj)
    {
        door = obj;
    }
   
    public override void TakeDamage(float amount)
    {
        if (isInvulnerable) return;

        float effectiveDamage = amount * (1 - resistance);
        currentHealth -= effectiveDamage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityFrames());
        }
    }

    private IEnumerator InvulnerabilityFrames()
    {
        isInvulnerable = true;

        float elapsedTime = 0f;
        while (elapsedTime < invulnerabilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true;
        isInvulnerable = false;
    }
}