using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    [SerializeField]
    private Score score;
    [SerializeField]
    public int GivenModifierAmount;

    [SerializeField]
    public Player[] players;

    private Player currentPlayer;

    [SerializeField]
    public Modifier[] possibleModifiers;

    [SerializeField]
    public int magBulletCount;
    [SerializeField]
    public Weapon weapon;
    [SerializeField]
    public InfoPanel infoPanel;

    public bool gameRunning = false;

    private int roundCount;
    private int turnCount;
    public int turnToBeat;
    public float startTime = 0.000f;
    public float elapsedTime = 0.000f;
    [SerializeField]
    public bool UseMLAgent = true;
    [SerializeField]
    public GameEnv gameEnv;
    private bool isPlayerInTurn = false;

    void Start()
    {
        if (gameEnv.isPlayedOnModel)
        {
            StartDelayed();
        }
    }

    private void StartDelayed()
    {
        StartCoroutine(StartGameAfterDelay(0.5f));

        IEnumerator StartGameAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            StartGame();
        }
    }

    void Update()
    {
        if (gameRunning)
        {
            elapsedTime = Time.time - startTime;
        }
        if (!isPlayerInTurn && gameRunning)
        {
            if (weapon.ammoCount == 0)
                NewRound();
            else
                StartTurn();
        }
    }

    public void StartGame()
    {
        startTime = Time.time;

        turnCount = 0;
        roundCount = 0;
        turnToBeat = 0;
        int startingPlayerIndex = Random.Range(0, players.Length);
        gameRunning = true;

        foreach (Player player in players)
            player.ResetPlayer();

        players[0].SetAgent(new HeuristicAgent(players[0]));

        if (UseMLAgent)
            players[1].SetAgent(players[1].GetComponent<MLAgent>());

        if (!players[1].isGamer)
            players[1].SetAgent(new MinMaxAgent(players[1]));

        DealModifiers();

        weapon.LoadWeapon(magBulletCount);
        currentPlayer = players[startingPlayerIndex];
        weapon.SetPlayerRoles(startingPlayerIndex);

        StartTurn();
    }

    public void EndGame()
    {
        Debug.Log("Time: " + elapsedTime);
        gameRunning = false;
        score.ScoreGame();
        if (UseMLAgent)
        {
            MLAgent agent = players[1].GetComponent<MLAgent>();
            agent.AddReward(score.CalculateScoreForPlayer(players[1]));
            agent.EndEpisode();
        }
    }

    public void NewRound()
    {
        roundCount++;
        Debug.Log("Round " + roundCount);
        DealModifiers();
        weapon.LoadWeapon(magBulletCount);
        if (!weapon.IsLastSelfShot())
            NextPlayer();
        StartTurn();
    }

    public void EndTurn()
    {
        turnToBeat++;

        if (!gameRunning) return;
        turnCount++;
        Debug.Log("Turn " + turnCount);
        Debug.Log("Ammo count: " + weapon.ammoCount);
        Debug.Log("Current player: " + currentPlayer.name);
        NextPlayer();
        isPlayerInTurn = false;
    }

    public void StartTurn()
    {
        infoPanel.ShowInfo("Round " + roundCount + " " + currentPlayer.name + "'s turn");
        isPlayerInTurn = true;
        currentPlayer.PlayTurn();
    }

    private void NextPlayer()
    {
        int currentPlayerIndex = System.Array.IndexOf(players, currentPlayer);
        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Length)
        {
            currentPlayerIndex = 0;
        }
        currentPlayer = players[currentPlayerIndex];
    }

    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void DealModifiers()
    {
        if (possibleModifiers == null || possibleModifiers.Length == 0)
        {
            Debug.LogError("No modifiers available!");
            return;
        }

        foreach (Player player in players)
        {
            for (int i = 0; i < GivenModifierAmount; i++)
            {
                int randomIndex = Random.Range(0, possibleModifiers.Length);
                player.AddModifier(possibleModifiers[randomIndex]);
            }
        }
    }
}
