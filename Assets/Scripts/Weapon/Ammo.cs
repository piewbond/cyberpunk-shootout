using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo
{
    [SerializeField]
    private bool isLive;
    public Ammo(bool isLive)
    {
        this.isLive = isLive;
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
