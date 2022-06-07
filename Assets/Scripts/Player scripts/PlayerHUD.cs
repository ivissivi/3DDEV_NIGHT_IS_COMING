using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Text healthText;

    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }

}
