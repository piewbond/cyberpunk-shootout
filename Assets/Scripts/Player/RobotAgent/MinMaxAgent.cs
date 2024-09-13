using System.Collections;
using System.Collections.Generic;
using System.Linq; // Add this using directive
using UnityEngine;

public class MinMaxAgent : Agent
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
        this.score = dealer.score;
    }

    public override void PlayTurn()
    {
        if (!DepthBySerialize)
        {
            maxDepth = weapon.GetAmmoList().Count;
        }

        

        List<PossibleMove> finalMoves = GetBestMove();

        foreach (PossibleMove move in finalMoves)
        {
            if (move.UseModifier)
            {
                player.UseModifier(move.Modifier);
            }
            else
            {
                player.Shoot(move.ShootEnemy);
            }
        }
    }



    private List<PossibleMove> GetBestMove()
    {
        List<PossibleMove> possibleMoves = new List<PossibleMove>();



        return possibleMoves;
    }

    private int Evaluate(bool isMaximizingPlayer)
    {
         return score.CalculateScoreForPlayer(isMaximizingPlayer? maxPlayer : minPlayer);
    }

    private int MinMax(int depth, bool isMaximizingPlayer)
    {
        if (depth == 0)
        {
            return Evaluate(isMaximizingPlayer);
        }

        if (isMaximizingPlayer)
        {
            int bestValue = int.MinValue;
            List<PossibleMove> possibleMoves = GetPossibleMoves();
            foreach (PossibleMove move in possibleMoves)
            {
                int value = MinMax(depth - 1, false);
                bestValue = Mathf.Max(bestValue, value);
            }
            return bestValue;
        }
        else
        {
            int bestValue = int.MaxValue;
            List<PossibleMove> possibleMoves = GetPossibleMoves();
            foreach (PossibleMove move in possibleMoves)
            {
                int value = MinMax(depth - 1, true);
                bestValue = Mathf.Min(bestValue, value);
            }
            return bestValue;
        }
    }

}
