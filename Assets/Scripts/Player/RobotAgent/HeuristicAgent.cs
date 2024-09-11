using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeuristicAgent : Agent
{
    private Player player;
    private Player enemy;
    private Dealer dealer;
    private Weapon weapon;
    public HeuristicAgent(Player player)
    {
        this.player = player;
        this.dealer = player.dealer;
        this.weapon = dealer.weapon;

        if (player.playerName == "Player1")
        {
            enemy = dealer.players[1];
        }
        else
        {
            enemy = dealer.players[0];
        }
    }

    public override void PlayTurn()
    {
        Debug.Log("Heuristic Agent playing turn");

        bool shootEnemy = true;
        int liveBulletCount = 0;
        int blankBulletCount = 0;
        List<Ammo> ammos = weapon.ammoList;

        foreach (Ammo ammo in ammos)
        {
            if (ammo.GetIsLive())
            {
                liveBulletCount++;
            }
            else
            {
                blankBulletCount++;
            }
        }

        if (liveBulletCount == 0)
        {
            shootEnemy = false;
        }

        if (liveBulletCount >= blankBulletCount)
        {
            shootEnemy = true;
        }

        List<Modifier> modifiersCopy = new List<Modifier>(player.GetModifiers());

        foreach (Modifier modifier in modifiersCopy)
        {
            switch (modifier.GetModifierType())
            {
                case ModifierType.DoubleAction:
                    player.UseModifier(modifier);
                    break;
                case ModifierType.Heal:
                    if (player.health < player.maxHealth)
                    {
                        player.UseModifier(modifier);
                    }
                    break;
                case ModifierType.IgnoreShield:
                    if (enemy.shield > 0 && shootEnemy)
                    {
                        player.UseModifier(modifier);
                    }
                    break;
                case ModifierType.MultiplyDamage:
                    if (shootEnemy || (player.knowsNextShot && player.isNextShotLive))
                    {
                        player.UseModifier(modifier);
                    }
                    break;
                case ModifierType.Shield:
                    if (liveBulletCount >= blankBulletCount)
                    {
                        player.UseModifier(modifier);
                    }
                    break;
                case ModifierType.SkipShot:
                    if (liveBulletCount <= blankBulletCount && !(player.knowsNextShot && player.isNextShotLive))
                    {
                        player.UseModifier(modifier);
                    }
                    break;
                case ModifierType.SpyBullet:
                    if (!player.knowsNextShot)
                    {
                        player.UseModifier(modifier);
                    }
                    if (player.knowsNextShot && player.isNextShotLive)
                        shootEnemy = true;
                    break;
                case ModifierType.Stun:
                    player.UseModifier(modifier);
                    break;
            }
        }

        player.Shoot(shootEnemy);
    }
}
