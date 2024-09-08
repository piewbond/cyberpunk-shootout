using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Modifier
{
    public int shieldAmount;
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
        Player player = dealer.GetCurrentPlayer();
        player.Shield(shieldAmount);
    }
}
