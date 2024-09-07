using System.Collections;
using System.Collections.Generic;
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
    private List<Modifier> modifiers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayTurn()
    {
        dealer.EndTurn();
    }

    public void TakeDamage(int damage)
    {
        if (shield > 0)
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

    public void Die()
    {
        Debug.Log("Player died");
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

    public void ShieldUp(int shieldAmount)
    {
        shield += shieldAmount;
    }

}
