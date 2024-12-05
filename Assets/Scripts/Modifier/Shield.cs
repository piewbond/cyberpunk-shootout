using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Modifier
{
    public int shieldAmount;


    public override void Apply()
    {
        player = dealer.GetCurrentPlayer();
        player.Shield(shieldAmount);
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.Shield;
    }
}
