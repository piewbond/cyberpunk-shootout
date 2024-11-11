using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private Player enemy;
    private Dealer dealer;
    private WeaponController weaponController;
    [SerializeField]
    private InfoPanel infoPanel;
    private Transform weaponSpot;

    void Start()
    {
        GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
        weaponController = weapon.GetComponent<WeaponController>();
        dealer = GameObject.FindGameObjectWithTag("Dealer").GetComponent<Dealer>();
        player = GetComponent<Player>();
        enemy = System.Array.Find(dealer.players, p => p != player);
        weaponSpot = transform.Find("WeaponSpot");
    }
    public void OnClick()
    {

        Debug.Log("Player clicked" + weaponController.isTargeting);
        if (weaponController.isTargeting)
        {
            weaponController.isTargeting = false;
            if (player.IsActivePlayer())
            {
                player.Shoot(false, false);
                infoPanel.ShowInfo("Player " + player.playerName + " shot the enemy");
            }
            else
            {
                enemy.Shoot(true, false);
                infoPanel.ShowInfo("Player " + player.playerName + " shot the enemy");
            }
        }
    }

    public Transform GetWeaponSpot()
    {
        return weaponSpot;
    }

    public bool IsActivePlayer()
    {
        return player.IsActivePlayer();
    }
}
