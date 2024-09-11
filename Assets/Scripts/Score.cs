using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    private string filePath = "Assets/Scripts/Score.txt";

    public void ScoreGame()
    {
        CalculateScore();
        Debug.Log("Score: " + finalScorePlayer1);
        Debug.Log("Score: " + finalScorePlayer2);
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

    public void WriteScoreToFile()
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Score constants: " + ScoreByRemainingHP + " " + ScoreByRemainingModifier + " " + turnToBeatScoreDivideBy);
            writer.WriteLine("Turn to beat: " + dealer.turnToBeat);
            foreach (Player player in dealer.players)
            {
                writer.WriteLine(player.GetPlayerInfoForScore() + " Score: " + (player.playerName == "Player1" ? finalScorePlayer1 : finalScorePlayer2));
            }
        }
    }
}
