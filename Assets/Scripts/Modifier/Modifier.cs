using System;
using UnityEngine;

public class Modifier : MonoBehaviour
{
    [SerializeField]
    protected Dealer dealer;
    protected Player player;
    [SerializeField]
    String ModifierName;
    [SerializeField]
    String ModifierDescription;
    void Start() { }
    public Modifier() { }
    public virtual void Apply() { }

    public virtual void Undo() { }
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

    internal Sprite GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    internal string GetModifierName()
    {
        return ModifierName;
    }

    internal string GetModifierDescription()
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
    InverzBullet,
}
