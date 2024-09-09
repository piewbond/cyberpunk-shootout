using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Modifier
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Apply()
    {
        GameObject dealerObject = GameObject.FindGameObjectWithTag("Dealer");
        Dealer dealer = dealerObject.GetComponent<Dealer>();
        foreach (Player player in dealer.players)
        {
            if (!player.IsActivePlayer())
            {
                player.SkipTurn();
            }
        }
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.Stun;
    }
}
