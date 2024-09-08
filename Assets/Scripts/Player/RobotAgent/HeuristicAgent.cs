using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeuristicAgent : Agent
{
    private Player player;
    private Dealer dealer;
    // Start is called before the first frame update
    public HeuristicAgent(Player player)
    {
        this.player = player;
        this.dealer = player.dealer;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void PlayTurn()
    {
        bool shootEnemy = Random.Range(0, 2) == 0;
        player.Shoot(shootEnemy);
    }
}
