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

    }

    public void EndGame()
    {

    }

    public void NewRound()
    {

    }

    public void EndRound()
    {

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
