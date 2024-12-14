using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;

public class Score : MonoBehaviour
{
    [SerializeField]
    public Dealer dealer;

    [SerializeField]
    private int ScoreByRemainingHP;
    [SerializeField]
    private int ScoreByRemainingModifier;
    [SerializeField]
    private int turnToBeatScoreDivideBy;

    private double finalScorePlayer1;
    private double finalScorePlayer2;

    private string player1ScorePath = "Assets/Scripts/Scores/Player1.txt";
    private string player2ScorePath = "Assets/Scripts/Scores/Player2.txt";
    private string timePath = "Assets/Scripts/Scores/Time.txt";
    private string turnPath = "Assets/Scripts/Scores/Turn.txt";

    public void ScoreGame()
    {
        CalculateScore();
        Debug.Log("Player 1 Score: " + finalScorePlayer1);
        Debug.Log("Player 2 Score: " + finalScorePlayer2);
    }

    private void CalculateScore()
    {
        finalScorePlayer1 = 0;
        finalScorePlayer2 = 0;
        foreach (Player player in dealer.players)
        {
            double finalScore = 0;
            finalScore += player.health * ScoreByRemainingHP;
            finalScore += player.GetModifiers().Count * ScoreByRemainingModifier;
            finalScore += dealer.turnToBeat / turnToBeatScoreDivideBy;
            if (player.playerName == "Player1")
            {
                finalScorePlayer1 = finalScore;
            }
            else
            {
                finalScorePlayer2 = finalScore;
            }
        }
        WriteScoreToFile();
    }

    public int CalculateScoreForPlayer(Player player)
    {
        int finalScore = 0;
        finalScore += player.health * ScoreByRemainingHP;
        finalScore -= player.GetModifiers().Count * ScoreByRemainingModifier;

        return finalScore;
    }

    public void WriteScoreToFile()
    {
        File.AppendAllText(player1ScorePath, finalScorePlayer1.ToString() + "\n");
        File.AppendAllText(player2ScorePath, finalScorePlayer2.ToString() + "\n");
        File.AppendAllText(timePath, dealer.elapsedTime.ToString() + "\n");
        File.AppendAllText(turnPath, dealer.turnToBeat.ToString() + "\n");
    }
}
