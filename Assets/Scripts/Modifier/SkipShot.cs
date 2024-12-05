using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipShot : Modifier
{
    [SerializeField]
    private Weapon weapon;
    public override void Apply()
    {
        weapon.SkipShot();
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.SkipShot;
    }
}
