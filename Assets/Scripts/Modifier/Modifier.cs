using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier : MonoBehaviour
{
    public virtual void Apply()
    {
        Debug.Log("Applying modifier to player");
    }

    public virtual ModifierType GetModifierType()
    {
        return ModifierType.DoubleAction;
    }

}

public enum ModifierType
{
    DoubleAction,
    Heal,
    IgnoreShield,
    MultiplyDamage,
    Shield,
    SkipShot,
    SpyBullet,
    Stun,
}
