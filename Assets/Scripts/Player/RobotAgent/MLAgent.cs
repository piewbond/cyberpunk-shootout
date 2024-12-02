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
    }

    public override void OnEpisodeBegin()
    {
        if (!isFirstEpisode)
            dealer.StartGame();
        isFirstEpisode = false;

        if (player.playerName == "Player1")
        {
            enemy = player.dealer.players[1];
        }
        else
        {
            enemy = player.dealer.players[0];
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        List<Modifier> modifiers = player.GetModifiers();
        if (weapon == null)
        {
            Debug.LogError("weapon null");
            Debug.Break();
        }

        sensor.AddObservation(weapon.GetLiveAmmoCount());
        sensor.AddObservation(weapon.GetBlankAmmoCount());

        sensor.AddObservation(player.health);
        sensor.AddObservation(player.shield);

        sensor.AddObservation(enemy.health);
        sensor.AddObservation(enemy.shield);

        sensor.AddObservation(player.knowsNextShot);
        sensor.AddObservation(player.isNextShotLive);

        List<float> modifierObservations = new List<float>();

        foreach (var modifier in modifiers)
        {
            switch (modifier.GetModifierType())
            {
                case ModifierType.Heal:
                    modifierObservations.Add(1.0f);
                    break;
                case ModifierType.DoubleAction:
                    modifierObservations.Add(2.0f);
                    break;
                case ModifierType.IgnoreShield:
                    modifierObservations.Add(3.0f);
                    break;
                case ModifierType.MultiplyDamage:
                    modifierObservations.Add(4.0f);
                    break;
                case ModifierType.Shield:
                    modifierObservations.Add(5.0f);
                    break;
                case ModifierType.SkipShot:
                    modifierObservations.Add(6.0f);
                    break;
                case ModifierType.SpyBullet:
                    modifierObservations.Add(7.0f);
                    break;
                case ModifierType.Stun:
                    modifierObservations.Add(8.0f);
                    break;
                case ModifierType.InverzBullet:
                    modifierObservations.Add(9.0f);
                    break;
                default:
                    modifierObservations.Add(0.0f);
                    break;
            }
        }

        int maxModifiers = dealer.GivenModifierAmount;
        while (modifierObservations.Count < maxModifiers)
        {
            modifierObservations.Add(-1.0f); // Pad with default value
        }

        sensor.AddObservation(modifierObservations);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("Action received +++++++++++++++++++++++++++++++++++++++++++++++");
        var continuousActions = actions.ContinuousActions;

        List<Modifier> useableModifiers = player.GetModifiers();
        List<Modifier> modifiersToUse = new List<Modifier>();
        if (useableModifiers.Count > 0)
        {
            for (int i = 0; i < dealer.GivenModifierAmount; i++)
            {
                if (i < useableModifiers.Count && continuousActions[i + 1] > 0.5f)
                {
                    //log modifierstouse count and useablemodifiers count
                    modifiersToUse.Add(useableModifiers[i]);
                }
                Debug.Log("Modifiers to use count: " + modifiersToUse.Count + " Useable modifiers count: " + useableModifiers.Count);
            }

            foreach (var modifier in modifiersToUse)
            {
                player.UseModifier(modifier);
            }
        }

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

        player.SetActivePlayer(false);
        dealer.EndTurn();
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

        // Heuristic for using modifiers (example: use all modifiers)
        for (int i = 1; i < continuousActionsOut.Length; i++)
        {
            continuousActionsOut[i] = 1.0f; // Use all modifiers
        }
    }

    public void PlayTurn()
    {
        RequestDecision();
    }
}
