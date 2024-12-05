using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverzBullet : Modifier
{
    [SerializeField]
    private Weapon weapon;

    public override void Apply()
    {
        weapon.InverzBullet();
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.InverzBullet;
    }
}
