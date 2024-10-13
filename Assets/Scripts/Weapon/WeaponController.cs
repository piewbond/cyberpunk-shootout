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
    public Transform _target;

    void Start()
    {
        playerControllers = gameEnv.GetComponentsInChildren<PlayerController>();
    }

    void Update()
    {
        if (isGrabbing && _target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, step);

            if (Vector3.Distance(transform.position, _target.position) < 0.001f)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                }
                isGrabbing = false;
                Debug.Log("Target reached");
            }
        }
    }

    public void OnClick()
    {
        infoPanel.ShowInfo("Choose target");
        Debug.Log("Weapon clicked");
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
                _target = playerController.GetWeaponSpot();
                break;
            }
        }
    }

}
