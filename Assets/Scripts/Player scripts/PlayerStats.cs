using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private PlayerHUD hud;
    private UIController ui;
    // Start is called before the first frame update
    private void Start()
    {
        hud = GetComponent<PlayerHUD>();
        ui = GetComponent<UIController>();
        InitVariables();
    }

    public override void Die()
    {
        base.Die();
        ui.SetHUD(false);
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health);
    }
}
