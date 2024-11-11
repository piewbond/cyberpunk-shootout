using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipShot : Modifier
{
    public override void Apply()
    {
        GameObject weaponObject = GameObject.FindGameObjectWithTag("Weapon");
        Weapon weapon = weaponObject.GetComponent<Weapon>();
        weapon.SkipShot();
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.SkipShot;
    }

    public override void Undo()
    {
        GameObject weaponObject = GameObject.FindGameObjectWithTag("Weapon");
        Weapon weapon = weaponObject.GetComponent<Weapon>();
        weapon.UndoShot();
    }
}
