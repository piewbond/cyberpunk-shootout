using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAction : Modifier
{
    public override void Apply()
    {
        player = dealer.GetCurrentPlayer();
        player.DoubleAction();
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.DoubleAction;
    }
}
