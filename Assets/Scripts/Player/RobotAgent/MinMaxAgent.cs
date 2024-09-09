using System.Collections;
using System.Collections.Generic;
using System.Linq; // Add this using directive
using UnityEngine;

public class MinMaxAgent : Agent
{
    private Dealer dealer;
    private Player player;
    private Weapon weapon;

    private int maxDepth;
    private bool shootEnemy = true;
    public MinMaxAgent(Player player)
    {
        this.player = player;
        this.dealer = player.dealer;
        this.weapon = dealer.weapon;
    }
    public override void PlayTurn()
    {
        maxDepth = weapon.GetAmmoList().Count;
        player.Shoot(shootEnemy);
    }
}
