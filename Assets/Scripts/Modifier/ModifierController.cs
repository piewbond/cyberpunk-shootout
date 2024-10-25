using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModifierController : MonoBehaviour
{
    [SerializeField]
    Modifier modifier;
    private Player player;
    private Dealer dealer;
    private bool isActive = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        GameObject dealerObject = GameObject.Find("Dealer");
        dealer = dealerObject.GetComponent<Dealer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        player = dealer.GetCurrentPlayer();
        if (player == null) return;
        if (player.HasModifier(modifier))
        {
            isActive = true;
            spriteRenderer.enabled = true;
        }
        else
        {
            isActive = false;
            spriteRenderer.enabled = false;
        }

    }

    public void OnClick()
    {
        if (player == null) return;
        if (!isActive) return;

        player.UseModifier(modifier);

    }

    public String GetModifierDescription()
    {
        return modifier.GetDescription();
    }

    public String GetModifierName()
    {
        return modifier.GetName();
    }
}
