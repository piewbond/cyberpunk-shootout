using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isTargeting = false;
    private Player player;
    private WeaponController weaponController;
    private InfoPanel infoPanel;

    void Start()
    {
        player = GetComponent<Player>();
        GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
        weaponController = weapon.GetComponent<WeaponController>();
        infoPanel = GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<InfoPanel>();
    }
    public void OnClick()
    {
        Debug.Log("Player clicked");
        if (isTargeting)
        {
            if (player.IsActivePlayer())
            {
                player.Shoot(false);
                infoPanel.ShowInfo("Player " + player.playerName + " shot the enemy");
            }
            else
            {
                player.Shoot(true);
                infoPanel.ShowInfo("Player " + player.playerName + " shot the enemy");
            }
            weaponController.isTargeting = false;
            isTargeting = false;
        }
    }
}
