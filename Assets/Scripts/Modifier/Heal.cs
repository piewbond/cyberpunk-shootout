using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Modifier
{
    private Player player;
    [SerializeField]
    private int healAmount;
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
        GameObject DealerObject = GameObject.FindGameObjectWithTag("Dealer");
        Dealer dealer = DealerObject.GetComponent<Dealer>();
        player = dealer.GetCurrentPlayer();
        player.Heal(healAmount);
    }

}
