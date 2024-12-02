using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Heal : Modifier
{
    [SerializeField]
    private int healAmount = 1;
    public override void Apply()
    {
        GameObject dealerObject = GameObject.FindGameObjectWithTag("Dealer");
        dealer = dealerObject.GetComponent<Dealer>();
        player = dealer.GetCurrentPlayer();
        player.Heal(healAmount);
        Debug.Log("Heal applied: " + player.playerName);
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.Heal;
    }

    public override void Undo()
    {
        GameObject dealerObject = GameObject.FindGameObjectWithTag("Dealer");
        dealer = dealerObject.GetComponent<Dealer>();
        player = dealer.GetCurrentPlayer();
        player.TakeDamage(healAmount, false);
    }

}
