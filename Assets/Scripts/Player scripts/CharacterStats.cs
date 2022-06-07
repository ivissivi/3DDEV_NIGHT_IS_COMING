using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;

    [SerializeField] protected bool isDead;
    // Start is called before the first frame update

    private void Start()
    {
        InitVariables();
    }

    public virtual void CheckHealth()
    {
        if(health <= 0)
        {
            health = 0;
            Die();
        }

        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Die()
    {
        isDead = true;
    }

    private void SetHealthTo(int healthToSet)
    {
        health = healthToSet;
        CheckHealth();
    }

    public void TakeDamage(int damage)
    {
        int healthAfterDamage = health - damage;
       SetHealthTo(healthAfterDamage);
    }

    public void Heal(int heal)
    {
        int healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }

    public void InitVariables()
    {
        maxHealth = 100;
        SetHealthTo(maxHealth);
        isDead = false;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
