using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Modifier : MonoBehaviour
{
    Button button;
    Dealer dealer;
    protected Player player;
    void Start() {
        button = GetComponent<Button>();
        GameObject dealerObject = GameObject.FindGameObjectWithTag("Dealer");
        dealer = dealerObject.GetComponent<Dealer>();
        player = dealer.GetCurrentPlayer();
    }
    void Update() 
    {
        if (player == dealer.GetCurrentPlayer()) 
        {
            button.interactable = true;
        }
    }
    public Modifier()
    {

    }
    public virtual void Apply()
    {
        Destroy(this);
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
