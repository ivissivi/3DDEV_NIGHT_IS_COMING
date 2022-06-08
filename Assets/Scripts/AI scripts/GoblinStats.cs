using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStats : CharacterStats
{
    [SerializeField] private int dmg;
    [SerializeField] public float attackSpeed;
    [SerializeField] private bool canAttack;

    // Start is called before the first frame update
    private void Start()
    {
        InitVariables();
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    public override void InitVariables()
    {
        maxHealth = 25;
        SetHealthTo(maxHealth);
        isDead = false;

        dmg = 5;
        attackSpeed = 1f;
        canAttack = true;
    }

    public void DoDamage(CharacterStats statsToDamage)
    {
        statsToDamage.TakeDamage(dmg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
