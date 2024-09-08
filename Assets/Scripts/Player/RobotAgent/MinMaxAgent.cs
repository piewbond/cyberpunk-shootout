using System.Collections;
using System.Collections.Generic;
using System.Linq; // Add this using directive
using UnityEngine;

public class MinMaxAgent : Agent
{
    private Dealer dealer;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public MinMaxAgent(Player player)
    {
        this.player = player;
        this.dealer = player.dealer;
    }
    public override void PlayTurn()
    {
        bool shootEnemy = true;
        player.Shoot(shootEnemy);
    }
}
