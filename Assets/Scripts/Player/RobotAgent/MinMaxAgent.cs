using System.Collections;
using System.Collections.Generic;
using System.Linq; // Add this using directive
using Unity.Barracuda;
using UnityEngine;

public class MinMaxAgent : IBaseAgent
{
    private Dealer dealer;
    private Player player;
    private Weapon weapon;
    [SerializeField]
    private int maxDepth;
    [SerializeField]
    private bool DepthBySerialize;

    private Player minPlayer;
    private Player maxPlayer;
    List<PossibleMove> bestMoves;

    double healScore;
    double doubleActionScore;
    double ignoreShieldScore;
    double multiplyDamageScore;
    double shieldScore;
    double skipShotScore;
    double spyBulletScore;
    double stunScore;
    double inverzScore;

    public MinMaxAgent(Player maxPlayer, Player minPlayer)
    {
        player = maxPlayer;
        this.maxPlayer = maxPlayer;
        this.minPlayer = minPlayer;
        dealer = player.dealer;
        weapon = dealer.weapon;
    }

    public void SetScores(MinMaxScores scores)
    {
        healScore = scores.healScore;
        doubleActionScore = scores.doubleActionScore;
        ignoreShieldScore = scores.ignoreShieldScore;
        multiplyDamageScore = scores.multiplyDamageScore;
        shieldScore = scores.shieldScore;
        skipShotScore = scores.skipShotScore;
        spyBulletScore = scores.spyBulletScore;
        stunScore = scores.stunScore;
        inverzScore = scores.inverzScore;
    }

    public void AskForDecision()
    {
        if (!DepthBySerialize)
        {
            maxDepth = weapon.ammoCount;
        }

        bestMoves = new List<PossibleMove>();

        MinMax(maxDepth, true, GetPossibleMovesFromModifiers(minPlayer.GetModifiers()), GetPossibleMovesFromModifiers(maxPlayer.GetModifiers()));
        if (bestMoves.Count == 0)
        {
            Debug.Log("Only shooting move possible");
            int liveCount = weapon.GetLiveAmmoCount();
            int blankCount = weapon.GetBlankAmmoCount();
            if (liveCount >= blankCount)
                bestMoves.Add(new PossibleMove(null, true, false));
            else
                bestMoves.Add(new PossibleMove(null, false, false));
        }
        foreach (var move in bestMoves)
        {
            if (move.UseModifier)
                Debug.Log("Using modifier: " + move.Modifier.GetModifierType());
            else
                Debug.Log("Shooting: " + (move.ShootEnemy ? "Enemy" : "Self"));

            if (player.knowsNextShot && player.isNextShotLive && !move.ShootEnemy)
            {
                player.Shoot(true);
                continue;
            }
            player.MakeMove(move);
        }
        player.SetActivePlayer(false);
        dealer.EndTurn();
    }

    private double EvaluateMoves(bool isMaximizingPlayer, List<PossibleMove> moves, int depth)
    {
        double score = isMaximizingPlayer ? 1 : -1;
        Player scorePlayer = isMaximizingPlayer ? maxPlayer : minPlayer;
        Player enemyPlayer = isMaximizingPlayer ? minPlayer : maxPlayer;

        PossibleMove shootMove = moves.FirstOrDefault(x => x.UseModifier == false);
        moves.Remove(shootMove);
        int liveCount = weapon.GetLiveAmmoCount();
        int blankCount = weapon.GetBlankAmmoCount();

        if ((liveCount == 0 && !shootMove.ShootEnemy))
        {
            return isMaximizingPlayer ? 10000 : -10000;
        }

        if (blankCount == 0 && !shootMove.ShootEnemy)
        {
            score = score / 10;
        }

        foreach (var move in moves)
        {
            if (move.UseModifier)
            {
                switch (move.Modifier.GetModifierType())
                {
                    case ModifierType.Heal:
                        if (scorePlayer.health < scorePlayer.maxHealth)
                        {
                            score *= healScore;
                        }
                        break;
                    case ModifierType.DoubleAction:
                        score *= doubleActionScore;
                        break;
                    case ModifierType.IgnoreShield:
                        if (enemyPlayer.shield > 0 && shootMove.ShootEnemy)
                            score *= ignoreShieldScore;
                        else 
                            score /= ignoreShieldScore;
                        break;
                    case ModifierType.MultiplyDamage:
                        if (shootMove.ShootEnemy)
                            score *= multiplyDamageScore;
                        else
                            score /= multiplyDamageScore;
                        break;
                    case ModifierType.Shield:
                        score *= shieldScore;
                        break;
                    case ModifierType.SkipShot:
                        score *= skipShotScore;
                        break;
                    case ModifierType.SpyBullet:
                        score *= spyBulletScore;
                        break;
                    case ModifierType.Stun:
                        score *= stunScore;
                        break;
                    case ModifierType.InverzBullet:
                        if (liveCount - depth / 2 >= blankCount - depth / 2)
                        {
                            if (shootMove.ShootEnemy)
                            {
                                score /= inverzScore;
                            }
                            else
                            {
                                score *= inverzScore;
                            }
                        }
                        break;
                }
            }
        }

        return score;
    }

    private double MinMax(int depth, bool isMaximizingPlayer, List<PossibleMove> minMoves = null, List<PossibleMove> maxMoves = null)
    {
        List<PossibleMove> movesToCheck = isMaximizingPlayer ? maxMoves : minMoves;

        double bestScore = isMaximizingPlayer ? int.MinValue : int.MaxValue;
        List<MoveSets> varitonsToCheck = new List<MoveSets>();
        List<MoveSets> sameScoreVariations = new List<MoveSets>();

        int n = movesToCheck.Count;
        for (int i = 0; i < (1 << n); i++)
        {
            List<PossibleMove> subset = new List<PossibleMove>();
            for (int j = 0; j < n; j++)
            {
                if ((i & (1 << j)) != 0)
                {
                    subset.Add(movesToCheck[j]);
                }
            }
            if (subset.Count > 0)
            {
                varitonsToCheck.Add(new MoveSets(subset, isMaximizingPlayer));
            }
        }

        foreach (MoveSets moveSets in varitonsToCheck)
        {
            foreach (PossibleMove move in movesToCheck)
            {
                for (int i = 0; i < 2; i++)
                {
                    List<PossibleMove> moves = new List<PossibleMove>(movesToCheck);
                    if (movesToCheck.Count > 1)
                        moves.Remove(move);
                    PossibleMove shootMove = new PossibleMove(null, i == 0, false);
                    moves.Add(shootMove); // Shooting enemy if i == 0, shooting self if i == 1
                    double score = EvaluateMoves(isMaximizingPlayer, moves, depth);
                    if (score == bestScore)
                    {
                        if (moves.Count != 0)
                        {
                            sameScoreVariations.Add(new MoveSets(moves, isMaximizingPlayer));
                        }
                    }
                    else if (isMaximizingPlayer)
                    {
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMoves = moves;
                            sameScoreVariations.Clear();
                            sameScoreVariations.Add(new MoveSets(moves, true));
                        }
                    }
                    else
                    {
                        if (score < bestScore)
                        {
                            bestScore = score;
                            sameScoreVariations.Clear();
                            sameScoreVariations.Add(new MoveSets(moves, false));
                        }
                    }
                }
            }

        }

        if (sameScoreVariations.Count > 1 && depth > 0)
        {
            bestScore = 0;
            foreach (MoveSets moveSet in sameScoreVariations)
            {
                if (moveSet.moves == null || moveSet.moves.Count == 0)
                {
                    Debug.LogError("No moves found");
                }
                if (!moveSet.moves.Last().UseModifier)
                {
                    moveSet.moves.Remove(moveSet.moves.Last());
                }

                List<PossibleMove> minmoves = new List<PossibleMove>();
                List<PossibleMove> maxmoves = new List<PossibleMove>();

                if (moveSet.isMaximizingPlayer)
                {
                    maxMoves = moveSet.moves;
                    minMoves = minmoves;
                }
                else
                {
                    minMoves = moveSet.moves;
                    maxMoves = maxmoves;
                }

                double score = MinMax(depth - 1, !isMaximizingPlayer, minMoves, maxMoves);
                if (isMaximizingPlayer)
                {
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMoves = moveSet.moves;
                    }
                }
                else
                {
                    if (score < bestScore)
                    {
                        bestScore = score;
                    }
                }
            }
        }

        return bestScore;
    }

    private List<PossibleMove> GetPossibleMovesFromModifiers(List<Modifier> modifiers)
    {
        List<PossibleMove> possibleMoves = new List<PossibleMove>();
        foreach (Modifier modifier in modifiers)
        {
            possibleMoves.Add(new PossibleMove(modifier, false, true));
        }
        return possibleMoves;
    }

    class MoveSets
    {
        public List<PossibleMove> moves;
        public bool isMaximizingPlayer;

        public MoveSets(List<PossibleMove> moves, bool isMaximizingPlayer)
        {
            this.moves = moves;
            this.isMaximizingPlayer = isMaximizingPlayer;
        }
    }
}
