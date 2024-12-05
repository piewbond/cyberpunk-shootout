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
    private bool isGrabbing = false;
    public float speed = 10f;
    public Transform weaponLocation;
    public Transform enemyLocation;

    void Start()
    {
        playerControllers = gameEnv.GetComponentsInChildren<PlayerController>();
    }

    void FixedUpdate()
    {
        if (isGrabbing && weaponLocation != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, weaponLocation.position, step);

            if (Vector3.Distance(transform.position, weaponLocation.position) < 0.001f)
            {
                isGrabbing = false;
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

                bool flipX = true;
                if (weaponLocation.position.x < enemyLocation.position.x)
                {
                    flipX = false;
                }
                spriteRenderer.flipX = flipX;
            }
        }
    }

    public void OnClick()
    {
        infoPanel.ShowInfo("Choose target");
        isTargeting = true;
        gameEnv.GetComponentsInChildren<PlayerController>();
    }

    public void GrabWeapon()
    {
        if (gameEnv.isPlayedOnModel)
            return;

        isGrabbing = true;

        foreach (PlayerController playerController in playerControllers)
        {
            if (playerController.IsActivePlayer())
            {
                weaponLocation = playerController.GetWeaponSpot();
            }
            else
            {
                enemyLocation = playerController.GetWeaponSpot();
            }
        }
    }

}
