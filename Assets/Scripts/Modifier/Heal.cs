using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Modifier
{
    public Player player;
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
        player.Heal(healAmount);
    }

}
