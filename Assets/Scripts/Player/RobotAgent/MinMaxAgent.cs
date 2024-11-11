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
    List<PossibleMove> bestMove;

    public MinMaxAgent(Player maxPlayer, Player minPlayer, Score score)
    {
        player = maxPlayer;
        this.maxPlayer = maxPlayer;
        this.minPlayer = minPlayer;
        this.score = score;
        dealer = player.dealer;
        weapon = dealer.weapon;
    }

    public void PlayTurn()
    {
        if (!DepthBySerialize)
        {
            maxDepth = weapon.ammoCount;
        }

        maxPlayer = player;

        MinMax(maxDepth, true);

        bestMove = new List<PossibleMove>();
        foreach (var move in bestMove)
        {
            player.MakeMove(move, true);
        }
    }

    private int Evaluate(bool isMaximizingPlayer)
    {
        return score.CalculateScoreForPlayer(isMaximizingPlayer ? maxPlayer : minPlayer);
    }

    private int MinMax(int depth, bool isMaximizingPlayer)
    {
        if (depth == 0 || player.HasWon() || player.HasLost())
        {
            return Evaluate(isMaximizingPlayer);
        }

        if (isMaximizingPlayer)
        {
            int maxEval = int.MinValue;
            List<PossibleMove> bestLocalMove = null;

            // Evaluate all possible combinations of moveindex -1
            var possibleMoves = GetPossibleMoves().Where(m => m.MoveIndex == -1).ToList();
            int combinations = 1 << possibleMoves.Count; // 2^possibleMoves.Count

            for (int i = 0; i < combinations; i++)
            {
                // Apply combination of moveindex -1
                for (int j = 0; j < possibleMoves.Count; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        player.MakeMove(possibleMoves[j], false);
                    }
                }

                // Evaluate moveindex 1
                foreach (var move in GetPossibleMoves().Where(m => m.MoveIndex == 1))
                {
                    player.MakeMove(move, false);
                    int eval = MinMax(depth - 1, false);
                    player.UndoMove(move);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        bestLocalMove = new List<PossibleMove>(possibleMoves.Where((m, idx) => (i & (1 << idx)) != 0));
                        bestLocalMove.Add(move);
                    }
                }

                // Undo combination of moveindex -1
                for (int j = 0; j < possibleMoves.Count; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        player.UndoMove(possibleMoves[j]);
                    }
                }
            }

            if (depth == maxDepth)
            {
                bestMove = bestLocalMove;
            }

            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            List<PossibleMove> bestLocalMove = null;

            // Evaluate all possible combinations of moveindex -1
            var possibleMoves = GetPossibleMoves().Where(m => m.MoveIndex == -1).ToList();
            int combinations = 1 << possibleMoves.Count; // 2^possibleMoves.Count

            for (int i = 0; i < combinations; i++)
            {
                // Apply combination of moveindex -1
                for (int j = 0; j < possibleMoves.Count; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        player.MakeMove(possibleMoves[j], false);
                    }
                }

                // Evaluate moveindex 1
                foreach (var move in GetPossibleMoves().Where(m => m.MoveIndex == 1))
                {
                    player.MakeMove(move, false);
                    int eval = MinMax(depth - 1, true);
                    player.UndoMove(move);
                    if (eval < minEval)
                    {
                        minEval = eval;
                        bestLocalMove = new List<PossibleMove>(possibleMoves.Where((m, idx) => (i & (1 << idx)) != 0));
                        bestLocalMove.Add(move);
                    }
                }

                // Undo combination of moveindex -1
                for (int j = 0; j < possibleMoves.Count; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        player.UndoMove(possibleMoves[j]);
                    }
                }
            }

            if (depth == maxDepth)
            {
                bestMove = bestLocalMove;
            }

            return minEval;
        }
    }

    private List<PossibleMove> GetPossibleMoves()
    {
        List<PossibleMove> possibleMoves = new List<PossibleMove>();
        foreach (Modifier modifier in player.GetModifiers())
        {
            possibleMoves.Add(new PossibleMove(-1, modifier, false, true));
        }
        possibleMoves.Add(new PossibleMove(1, null, false, false));
        possibleMoves.Add(new PossibleMove(1, null, true, false));
        return possibleMoves;
    }

}
