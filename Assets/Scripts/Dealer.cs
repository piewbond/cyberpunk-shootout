using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
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

    private bool gameRunning;

    private int roundCount;
    private int turnCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        turnCount = 0;
        roundCount = 0;
        gameRunning = false;
        foreach (Player player in players)
        {
            player.health = player.maxHealth;
            player.shield = 0;
        }
        DealModifiers();
        weapon.LoadWeapon(magBulletCount);
        currentPlayer = players[0];
    }

    public void EndGame()
    {

    }

    public void NewRound()
    {
        roundCount++;
        DealModifiers();
        weapon.LoadWeapon(magBulletCount);
    }

    public void EndTurn()
    {
        turnCount++;
        NextPlayer();
        if (weapon.ammoCount == 0)
        {
            NewRound();
        }
        else
        {
            StartTurn();
        }
    }

    public void StartTurn()
    {
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

    public void DealModifiers()
    {
        foreach (Player player in players)
        {
            for (int i = 0; i < GivenModifierAmount; i++)
            {
                player.AddModifier(possibleModifiers[Random.Range(0, possibleModifiers.Length)]);
            }
        }
    }

}
