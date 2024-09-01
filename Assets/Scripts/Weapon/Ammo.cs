using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private bool isLive;
    // Start is called before the first frame update
    void Start()
    {
        isLive = Random.Range(0, 2) == 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InverzAmmo()
    {
        isLive = !isLive;
    }

    public bool GetIsLive()
    {
        return isLive;
    }
}
