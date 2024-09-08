using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void PlayTurn()
    {

    }

    public Player SetPlayer(Player player)
    {
        this.player = player;
        return player;
    }
}
