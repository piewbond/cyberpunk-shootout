using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    private TextMeshProUGUI textMeshPro;
    [SerializeField]
    public Image healthBar;
    void Update()
    {
        int health = player.health;
        textMeshPro.text = health.ToString();
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)player.health / player.maxHealth;
    }
}
