using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreShield : Modifier
{
    private Weapon weapon;
    void Start()
    {
        GameObject weaponObject = GameObject.FindGameObjectWithTag("Weapon");
        weapon = weaponObject.GetComponent<Weapon>();
    }

    public override void Apply()
    {
        weapon.ignoreShield = true;
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.IgnoreShield;
    }

    public override void Undo()
    {
        weapon.ignoreShield = false;
    }
}
