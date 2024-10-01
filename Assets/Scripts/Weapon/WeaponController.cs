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
    PlayerController[] playerControllers;
    public bool isTargeting = false;
    void Start()
    {
        playerControllers = gameEnv.GetComponentsInChildren<PlayerController>();
    }

    public void OnClick()
    {
        infoPanel.ShowInfo("Choose target");
        Debug.Log("Weapon clicked");
        isTargeting = true;
        gameEnv.GetComponentsInChildren<PlayerController>();
        foreach (PlayerController playerController in playerControllers)
        {
            playerController.isTargeting = true;
        }
    }

    public void GrabWeapon()
    {
        if (gameEnv.isPlayedOnModel)
            return;
        //TODO implement weapon grab
    }
}
