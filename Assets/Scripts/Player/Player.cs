using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int MaxModifierAmount;

    [SerializeField]
    public string playerName;
    [SerializeField]
    public bool isGamer;
    [SerializeField]
    NextShotPanel nextShotPanel;

    public int health;
    public int maxHealth;
    public int shield;
    public bool knowsNextShot;
    public bool isNextShotLive;
    private bool skipTurn = false;
    private bool doubleAction = false;
    private bool activePlayer = false;
    public IBaseAgent agent;

    [SerializeField]
    public Dealer dealer;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private List<Modifier> modifiers;

    [SerializeField]
    private WeaponController weaponController;
    private GameEnv game;

    void Start()
    {
        modifiers = new List<Modifier>();
        game = FindObjectOfType<GameEnv>();
    }

    void Update()
    {

    }

    public void PlayTurn()
    {
        activePlayer = true;

        Debug.Log(playerName + " playing turn");
        if (skipTurn)
        {
            skipTurn = false;
            dealer.EndTurn();
            return;
        }

        if (game.isPlayedOnModel || !isGamer)
        {
            agent.PlayTurn();
            dealer.EndTurn();
            Debug.Log(playerName + " played turn");
            activePlayer = false;
            return;
        }

        weaponController.GrabWeapon();
    }

    public void TakeDamage(int damage, bool ignoreShield)
    {
        Debug.Log(playerName + " took " + damage + " damage" + " health: " + health);
        if (shield > 0)
        {
            if (ignoreShield)
            {
                health -= damage;
            }
            else
            {
                shield -= damage;
                if (shield < 0)
                {
                    health += shield;
                    shield = 0;
                }
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
        if (isGamer)
        {
            Debug.Log(playerName + " died.");
            dealer.EndGame();
            return;
        }
        Debug.Log(playerName + " died. Agent type: " + agent.GetType());
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

    public void SetAgent(IBaseAgent agent)
    {
        this.agent = agent;
    }

    public void Shoot(bool shootEnemy)
    {
        if (doubleAction)
        {
            doubleAction = false;
            weapon.Shoot(shootEnemy);
        }
        weapon.Shoot(shootEnemy);
        knowsNextShot = false;
        if (isGamer)
        {
            Debug.Log(playerName + " shot");
            activePlayer = false;
            dealer.EndTurn();
        }
    }

    public List<Modifier> GetModifiers()
    {
        return modifiers;
    }

    public void UseModifier(Modifier modifier)
    {
        Debug.Log(playerName + " used " + modifier.GetModifierType());
        if (doubleAction)
        {
            doubleAction = false;
            modifier.Apply();
        }
        modifier.Apply();
        modifiers.Remove(modifier);
    }

    public void SkipTurn()
    {
        skipTurn = true;
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
        nextShotPanel.ShowNext(isNextShotLive);
    }

    public string GetPlayerInfoForScore()
    {
        if (agent != null)
            return playerName + " with agent: " + agent.GetType();
        return playerName;
    }

    public IBaseAgent GetAgent()
    {
        return agent;
    }

    public bool HasModifier(Modifier modifier)
    {
        return modifiers.Contains(modifier);
    }

    public void DoubleAction()
    {
        doubleAction = true;
    }
}
