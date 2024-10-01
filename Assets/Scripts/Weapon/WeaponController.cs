using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    GameEnv gameEnv;
    [SerializeField]
    private InfoPanel infoPanel;
    private Weapon weapon;
    PlayerController[] playerControllers;
    public bool isTargeting = false;
    void Start()
    {
        weapon = GetComponent<Weapon>();
        playerControllers = gameEnv.GetComponentsInChildren<PlayerController>();
    }

    public void OnClick()
    {
        infoPanel.ShowInfo("Choose target");
        Debug.Log("Weapon clicked");
        isTargeting = !isTargeting;
        gameEnv.GetComponentsInChildren<PlayerController>();
        foreach (PlayerController playerController in playerControllers)
        {
            playerController.isTargeting = !playerController.isTargeting;
        }
    }

    public void GrabWeapon()
    {
        if (gameEnv.isPlayedOnModel)
            return;
        //TODO implement weapon grab
    }
}
