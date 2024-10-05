using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAction : Modifier
{

    public DoubleAction() : base()
    { }
    public override void Apply()
    {
        GameObject dealerObject = GameObject.FindGameObjectWithTag("Dealer");
        Dealer dealer = dealerObject.GetComponent<Dealer>();
        Player player = dealer.GetCurrentPlayer();
        player.DoubleAction();
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.DoubleAction;
    }
}
