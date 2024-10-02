using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo
{
    private bool isLive;
    public Ammo()
    {
        isLive = Random.Range(0, 2) == 0;
    }

    public void InverzAmmo()
    {
        isLive = !isLive;
    }

    public bool GetIsLive()
    {
        return isLive;
    }

    public void SetIsLive(bool isLive)
    {
        this.isLive = isLive;
    }
}
