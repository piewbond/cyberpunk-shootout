using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanAgent : IBaseAgent
{
    Player player;
    Dealer dealer;
    Weapon weapon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTurn() 
    {

    }

    public HumanAgent(Player player)
    {
        this.player = player;
        this.dealer = player.dealer;
        this.weapon = dealer.weapon;
    }

    public void Shoot(bool shootEnemy) 
    {
        weapon.Shoot(shootEnemy);
        SetPlayerButton(false);
        dealer.EndTurn();
    }

    public void AimGun() 
    {
        SetPlayerButton(true);
    }

    private void SetPlayerButton(bool interactable) 
    {
                foreach(Player p in dealer.players)
        {
            Button button = p.GetComponent<Button>();
            button.interactable = interactable;
        }
    }

}
