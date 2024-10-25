using System;
using UnityEngine;

public class Modifier : MonoBehaviour
{
    protected Dealer dealer;
    protected Player player;
    [SerializeField]
    String ModifierName;
    [SerializeField]
    String ModifierDescription;
    void Start() { }
    public Modifier() { }
    public virtual void Apply() { }

    public virtual ModifierType GetModifierType()
    {
        return ModifierType.DoubleAction;
    }

    public String GetName()
    {
        return ModifierName;
    }
    public String GetDescription()
    {
        return ModifierDescription;
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
