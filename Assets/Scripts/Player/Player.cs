using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int shield;
    [SerializeField]
    public Dealer dealer;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private int MaxModifierAmount;
    [SerializeField]
    private List<Modifier> modifiers;
    private Agent agent;

    [SerializeField]
    public string playerName;
    private bool skipTurn = false;
    private bool activePlayer = false;
    public bool knowsNextShot;
    public bool isNextShotLive;
    // Start is called before the first frame update

    void Start()
    {
        modifiers = new List<Modifier>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayTurn()
    {
        Debug.Log(playerName + " playing turn");
        if (skipTurn)
        {
            skipTurn = false;
            dealer.EndTurn();
            return;
        }
        agent.PlayTurn();
        Debug.Log(playerName + " played turn");
        dealer.EndTurn();
    }

    public void TakeDamage(int damage, bool ignoreShield)
    {
        Debug.Log(playerName + " took " + damage + " damage" + " health: " + health);
        if (shield > 0 && !ignoreShield)
        {
            shield -= damage;
            if (shield < 0)
            {
                health += shield;
                shield = 0;
            }
        }
        else
        {
            health -= damage;
        }
        if (health <= 0)
        {
            Die();
        }
    }

    public void ResetPlayer()
    {
        health = maxHealth;
        shield = 0;
        modifiers.Clear();
        knowsNextShot = false;
        isNextShotLive = false;
    }

    public void Die()
    {
        Debug.Log(playerName + " died. type: " + agent.GetType());
        dealer.EndGame();
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void AddModifier(Modifier modifier)
    {
        if (modifiers.Count >= MaxModifierAmount)
        {
            return;
        }
        modifiers.Add(modifier);
    }

    public void RemoveModifier(Modifier modifier)
    {
        modifiers.Remove(modifier);
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void Shield(int shieldAmount)
    {
        shield += shieldAmount;
    }

    public void SetAgent(Agent agent)
    {
        this.agent = agent;
    }

    public void Shoot(bool shootEnemy)
    {
        weapon.Shoot(shootEnemy);
        knowsNextShot = false;
    }

    public List<Modifier> GetModifiers()
    {
        return modifiers;
    }

    public void UseModifier(Modifier modifier)
    {
        Debug.Log(playerName + " used " + modifier.GetModifierType());
        modifier.Apply();
        modifiers.Remove(modifier);
    }

    public void SkipTurn()
    {
        this.skipTurn = true;
    }

    public void SetActivePlayer(bool active)
    {
        activePlayer = active;
    }

    public bool IsActivePlayer()
    {
        return activePlayer;
    }

    public void SpyBullet()
    {
        knowsNextShot = weapon.isNextShotLive();
        isNextShotLive = weapon.isNextShotLive();
    }

    public string GetPlayerInfoForScore()
    {
        return playerName + " with agent: " + agent.GetType();
    }
}
