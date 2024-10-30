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
    private TextMeshProUGUI healthText;
    [SerializeField]
    private TextMeshProUGUI shieldText;
    [SerializeField]
    public Image healthBar;
    void Update()
    {
        healthText.text = player.health.ToString();
        shieldText.text = player.shield.ToString();
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)player.health / player.maxHealth;
    }
}
