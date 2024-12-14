using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyBullet : Modifier
{
    public override void Apply()
    {
        player = dealer.GetCurrentPlayer();
        player.SpyBullet();
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.SpyBullet;
    }
}
