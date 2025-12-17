using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Stats, ITakeDamage
{
    [Header("Stats")]
    public int pointValue;
    public int maxAmmo;
    private int currAmmo;

    public Transform respawnPoint;
    public GameObject hitScreen;

    public float invulnerabilityDuration = 0.5f;
    public float blinkInterval = 0.075f;
    private bool isInvulnerable = false;

    public Slider hpSlider;
    Coroutine hpDrain;

    protected override void Start()
    {
        //assign stuff
        maxHealth = 100f;
        damage = 25f;
        speed = 3f;
        resistance = 0f;
        pointValue = 25;

        
        currentHealth = maxHealth;
        currAmmo = maxAmmo;
        SetHP(this);
    }

    public void SetHP(PlayerStats player)
    {
        hpSlider.maxValue = player.maxHealth;
        hpSlider.value = player.currentHealth;
    }
    //Function to update HP on HUD
    public void UpdateHP(float hp)
    {
        //Stop any existing HP drain coroutine
        if (hpDrain != null)
        {
            StopCoroutine(hpDrain);
        }
        hpDrain = StartCoroutine(DrainHP(hpSlider.value, hp, 0.4f));
    }

    //Function to animate HP drain over time
    IEnumerator DrainHP(float from, float to, float duration)
    {
        float elapsed = 0f;
        // While elapsed time is less than duration, we find HP value
        while (elapsed < duration)
        {
            // Increment elapsed time
            elapsed += Time.deltaTime;
            //Lessen HP value based on elapsed time, using Mathf.Lerp for smooth transition
            hpSlider.value = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        //Ensure HP is set to final value
        hpSlider.value = to;
        hpDrain = null;
    }
    public override void Die()
    {
        // Handle death
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        // Add respawn delay or effects here if needed
        yield return new WaitForSeconds(0f);
        // Respawn logic
        currentHealth = maxHealth;
        UpdateHP(currentHealth);
        //Reset Ammo As well
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
    }
    
    public override void TakeDamage(float amount)
    {
        if (isInvulnerable) return;


        StartCoroutine(DamageFlashRoutine());

        float effectiveDamage = amount * (1 - resistance);
        currentHealth -= effectiveDamage;

        UpdateHP(currentHealth);
        //healthText.text = "Health: " + currentHealth.ToString("F0");
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityFrames());
        }
    }

    public void SetAmmoMax()
    {
        currAmmo = maxAmmo;
    }

    public void ChangeAmmo(int value) //use negatives to decrease
    {
        currAmmo += value;
    }

    public int GetCurrentAmmo()
    {
        return currAmmo;
    }

    private IEnumerator DamageFlashRoutine()
    {
        float flashDuration = 0.5f;
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            var color = hitScreen.GetComponent<Image>().color;
            color.a = Mathf.Lerp(invulnerabilityDuration, 0f, elapsedTime / flashDuration);
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