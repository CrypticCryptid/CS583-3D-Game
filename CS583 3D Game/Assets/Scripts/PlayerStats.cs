using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Stats, ITakeDamage
{
    [Header("Stats")]
    public int pointValue;

    public Transform respawnPoint;
    public GameObject hitScreen;
    public TextMeshProUGUI healthText;

    public float invulnerabilityDuration = 0.5f;
    public float blinkInterval = 0.075f;
    private bool isInvulnerable = false;

    protected override void Start()
    {
        //assign stuff
        maxHealth = 100f;
        damage = 0f;
        speed = 1f;
        resistance = 0f;
        pointValue = 25;

        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth.ToString("F0");
    }

    public override void Die()
    {
        // Handle death
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        // Add respawn delay or effects here if needed
        yield return new WaitForSeconds(0f);
        // Respawn logic
        currentHealth = maxHealth;

        healthText.text = "Health: " + currentHealth.ToString("F0");
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;

        Stats enemyStats = other.gameObject.GetComponent<Stats>();
        if (enemyStats == null)
            enemyStats = other.gameObject.GetComponentInParent<Stats>();

        if (enemyStats == null)
        {
            return;
        }

        TakeDamage(enemyStats.damage);
    }
    public override void TakeDamage(float amount)
    {
        if (isInvulnerable) return;


        StartCoroutine(DamageFlashRoutine());

        float effectiveDamage = amount * (1 - resistance);
        currentHealth -= effectiveDamage;

        healthText.text = "Health: " + currentHealth.ToString("F0");
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityFrames());
        }
    }

    private IEnumerator DamageFlashRoutine()
    {
        float flashDuration = 0.5f;
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            var color = hitScreen.GetComponent<Image>().color;
            color.a = Mathf.Lerp(0.5f, 0f, elapsedTime / flashDuration);
            hitScreen.GetComponent<Image>().color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        var finalColor = hitScreen.GetComponent<Image>().color;
        finalColor.a = 0f;
        hitScreen.GetComponent<Image>().color = finalColor;
    }

    private IEnumerator InvulnerabilityFrames()
    {
        isInvulnerable = true;

        float elapsedTime = 0f;
        while (elapsedTime < invulnerabilityDuration)
        {
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        isInvulnerable = false;
    }
}