using System.Collections;
using System.Collections.Generic;
using System.Linq; // Add this using directive
using UnityEngine;

public class MinMaxAgent : IBaseAgent
{
    private Dealer dealer;
    private Player player;
    private Weapon weapon;
    [SerializeField]
    private Score score;

    [SerializeField]
    private int maxDepth;
    [SerializeField]
    private bool DepthBySerialize;

    private Player minPlayer;
    private Player maxPlayer;

    public MinMaxAgent(Player player)
    {
        this.player = player;
        this.dealer = player.dealer;
        this.weapon = dealer.weapon;
    }

    public void PlayTurn()
    {
        player.Shoot(true);
    }



    private List<PossibleMove> GetBestMove()
    {
        List<PossibleMove> possibleMoves = new List<PossibleMove>();



        return possibleMoves;
    }

    private int Evaluate(bool isMaximizingPlayer)
    {
        return score.CalculateScoreForPlayer(isMaximizingPlayer ? maxPlayer : minPlayer);
    }

}
