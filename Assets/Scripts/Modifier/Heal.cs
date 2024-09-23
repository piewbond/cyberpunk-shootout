using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Heal : Modifier
{
    [SerializeField]
    private int healAmount;
    public override void Apply()
    {
        player.Heal(healAmount);
        base.Apply();
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.Heal;
    }

}
