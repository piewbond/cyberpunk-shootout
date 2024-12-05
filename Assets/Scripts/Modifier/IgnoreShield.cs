using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreShield : Modifier
{
    [SerializeField]
    private Weapon weapon;

    public override void Apply()
    {
        weapon.ignoreShield = true;
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.IgnoreShield;
    }
}
