using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, ITakeDamage
{
    //[!!!] TEMPORARY
    protected Renderer rend;

    [Header("Stats")]
    public float maxHealth = 100f;
    protected float currentHealth;
    public float damage = 10f;
    public float speed = 3f;
    public float resistance = 0f;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        
        //[!!!] TEMPORARY
        // rend = GetComponent<Renderer>();
        // if (rend != null)
        // {
        //     rend.material.color = Color.green;
        // }
    }

    public virtual void Die()
    {
        GameManager.Instance.ScoreUpdate(transform.tag);
    }

    public virtual void TakeDamage(float amount)
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
