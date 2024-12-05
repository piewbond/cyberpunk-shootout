using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Modifier
{
    public override void Apply()
    {
        foreach (Player player in dealer.players)
        {
            if (!player.IsActivePlayer())
            {
                player.SkipTurn(true);
            }
        }
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.Stun;
    }
}
