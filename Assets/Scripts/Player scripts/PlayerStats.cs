using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private PlayerHUD hud;
    // Start is called before the first frame update
    void Start()
    {
        hud = GetComponent<PlayerHUD>();
        InitVariables();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }
}
