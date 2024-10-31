using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MLAgent : Agent, IBaseAgent
{
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Player enemy;
    [SerializeField]
    private Dealer dealer;
    private bool isFirstEpisode = true;
    void Start()
    {
        // Link to the player and weapon
        player = GetComponent<Player>();
        weapon = player.GetComponent<Weapon>();
        dealer = player.dealer;

        // Assume enemy is the other player in the dealer
        if (player.playerName == "Player1")
        {
            enemy = player.dealer.players[1];
        }
        else
        {
            enemy = player.dealer.players[0];
        }
    }

    public override void OnEpisodeBegin()
    {
        if (!isFirstEpisode)
            dealer.StartGame();
        isFirstEpisode = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (weapon == null)
            Debug.LogError("weapon null");
        //TODO: Fix weapon nullreference
        // sensor.AddObservation(weapon.GetLiveAmmoCount());
        // sensor.AddObservation(weapon.GetBlankAmmoCount());

        // Optionally: Add observations like player's health, shield, etc.
        sensor.AddObservation(player.health);
        sensor.AddObservation(player.shield);

        //Add observatoin enemy health and shield
        sensor.AddObservation(enemy.health);
        sensor.AddObservation(enemy.shield);

        // Observe whether the player knows if the next shot is live or not
        sensor.AddObservation(player.knowsNextShot);
        sensor.AddObservation(player.isNextShotLive);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Get the decision to shoot the enemy (1) or shoot self (0)
        var continuousActions = actions.ContinuousActions;
        float shootEnemy = continuousActions[0];

        if (shootEnemy > 0.5f)
        {
            // Shoot the enemy
            player.Shoot(true);
        }
        else
        {
            // Shoot self
            player.Shoot(false);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Simple heuristic: shoot the enemy if there are more live bullets than blank
        var continuousActionsOut = actionsOut.ContinuousActions;
        int liveBulletCount = weapon.GetLiveAmmoCount();
        int blankBulletCount = weapon.GetBlankAmmoCount();

        // Heuristic: shoot the enemy if live bullets are equal to or more than blanks
        if (liveBulletCount >= blankBulletCount)
        {
            continuousActionsOut[0] = 1.0f;  // Shoot the enemy
        }
        else
        {
            continuousActionsOut[0] = 0.0f;  // Shoot self
        }
    }

    public void PlayTurn()
    {
        RequestDecision();
        player.dealer.EndTurn();
    }
}
