using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private Player enemy;
    private Dealer dealer;
    private WeaponController weaponController;
    private InfoPanel infoPanel;

    void Start()
    {
        GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
        weaponController = weapon.GetComponent<WeaponController>();
        infoPanel = GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<InfoPanel>();
        dealer = GameObject.FindGameObjectWithTag("Dealer").GetComponent<Dealer>();
        player = GetComponent<Player>();
        enemy = System.Array.Find(dealer.players, p => p != player);
    }
    public void OnClick()
    {

        Debug.Log("Player clicked" + weaponController.isTargeting);
        if (weaponController.isTargeting)
        {
            weaponController.isTargeting = false;
            if (player.IsActivePlayer())
            {
                player.Shoot(false);
                infoPanel.ShowInfo("Player " + player.playerName + " shot the enemy");
            }
            else
            {
                enemy.Shoot(true);
                infoPanel.ShowInfo("Player " + player.playerName + " shot the enemy");
            }
        }
    }
}
