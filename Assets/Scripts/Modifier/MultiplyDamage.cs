using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyDamage : Modifier
{
    [SerializeField]
    public int damageAmount;
    [SerializeField]
    private Weapon weapon;

    public override void Apply()
    {
        weapon.MultiplyDamage(damageAmount);
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.MultiplyDamage;
    }

    public override void Undo()
    {
        weapon.MultiplyDamage(1);
    }
}
