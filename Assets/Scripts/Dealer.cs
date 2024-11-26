using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
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

    [SerializeField]
    InventoryPage[] inventoryPages;
    [SerializeField]
    NextShotPanel nextShotPanel;

    //PVP
    //PVMIN
    //PVML
    //PVHE
    private int GameMode;
    [SerializeField]
    private int DefaultModeToStart;

    void Start()
    {
        GameMode = PlayerPrefs.GetInt("GameMode", DefaultModeToStart);
        GameMode = 1;
        Debug.Log("GameMode: " + GameMode);

        if (gameEnv.isPlayedOnModel)
        {
            StartDelayed();
        }
    }

    private void StartDelayed()
    {
        Debug.Log("Starting game after delay");
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
        // currentPlayer = players[startingPlayerIndex];
        currentPlayer = players[0];

        foreach (Player player in players)
            player.ResetPlayer();

        if (UseMLAgent)
        {
            Debug.Log("Heuristic set");
            players[0].SetAgent(new HeuristicAgent(players[0]));
            players[1].SetAgent(players[1].GetComponent<MLAgent>());
        }
        else
        {
            switch (GameMode)
            {
                case 0:
                    UseMLAgent = false;
                    foreach (Player player in players)
                    {
                        player.isGamer = true;
                    }
                    break;
                case 1:
                    players[1].SetAgent(new MinMaxAgent(players[1], players[0], score));
                    break;
                case 2:
                    players[1].SetAgent(players[1].GetComponent<MLAgent>());
                    break;
                case 3:
                    players[1].SetAgent(new HeuristicAgent(players[1]));
                    break;
                default:
                    break;
            }
        }

        DealModifiers();

        weapon.LoadWeapon(magBulletCount);
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
        if (!weapon.IsLastSelfShot())
        {
            NextPlayer();
        }
        isPlayerInTurn = false;
        nextShotPanel.ResetPanel();
    }

    public void StartTurn()
    {
        if (isPlayerInTurn)
            return;

        if (infoPanel == null)
        {
            Debug.LogError("No info panel found!");
            return;
        }
        if (currentPlayer == null)
        {
            Debug.LogError("No current player found!");
            return;
        }

        infoPanel.ShowInfo("R: " + roundCount + " T: " + turnCount + " " + currentPlayer.name + "'s turn");
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

        foreach (InventoryPage inventoryPage in inventoryPages)
        {
            inventoryPage.UpdateModifers();
        }
    }
}
