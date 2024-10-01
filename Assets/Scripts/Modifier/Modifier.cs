using UnityEngine;

public class Modifier : MonoBehaviour
{
    protected Dealer dealer;
    protected Player player;
    void Start() { }
    public Modifier() { }
    public virtual void Apply() { }

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
