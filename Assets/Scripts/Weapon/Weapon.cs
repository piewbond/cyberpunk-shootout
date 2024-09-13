using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<Ammo> ammoList;
    public int ammoCount;
    [SerializeField]
    public int damage;
    public bool ignoreShield;
    [SerializeField]
    public List<Player> players;
    private Player targetPlayer;
    private Player shooterPlayer;
    private bool shootEnemy;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(bool shootEnemy)
    {
        this.shootEnemy = shootEnemy;

        if (ammoCount > 0)
        {
            ammoCount--;
            if (ammoList[0].GetIsLive())
            {
                Debug.Log("Target player: " + targetPlayer.playerName + "\nShooter player: " + shooterPlayer.playerName);
                if (shootEnemy)
                {
                    targetPlayer.TakeDamage(damage, ignoreShield);
                    SwitchPlayerRole();
                }
                else
                {
                    shooterPlayer.TakeDamage(damage, ignoreShield);
                }
                ammoList.RemoveAt(0);
            }
            else
            {
                Debug.Log("Ammo is not live");
                //Do whiff animation here
            }
            Debug.Log("Shoot " + ammoCount);
        }
        damage = 1;
    }

    private void SwitchPlayerRole()
    {
        Player temp = targetPlayer;
        targetPlayer = shooterPlayer;
        shooterPlayer = temp;
    }

    public void SetPlayerRoles(int targetPlayerIndex)
    {
        targetPlayer = players[targetPlayerIndex];
        shooterPlayer = players[(targetPlayerIndex + 1) % players.Count];
    }

    public void LoadWeapon(int ammoCount)
    {
        this.ammoCount = ammoCount;
        ammoList = new List<Ammo>();
        for (int i = 0; i < ammoCount; i++)
        {
            ammoList.Add(new Ammo());
        }

        int blankCount = 0;
        foreach (Ammo ammo in ammoList)
            if (!ammo.GetIsLive())
                blankCount++;

        if (blankCount == ammoCount)
        {
            ammoList[Random.Range(0, ammoList.Count)].InverzAmmo();
        }
        Debug.Log("Weapon loaded: " + ammoCount);
    }

    public void MultiplyDamage(int damageMultiplier)
    {
        damage *= damageMultiplier;
    }

    public bool IsLastSelfShot()
    {
        return !shootEnemy;
    }

    public void SkipShot()
    {
        //Shooting at the ceiling animation here
        ammoList.RemoveAt(0);
    }

    public bool isNextShotLive()
    {
        return ammoList[0].GetIsLive();
    }

    public List<Ammo> GetAmmoList()
    {
        return ammoList;
    }

    public int GetLiveAmmoCount()
    {
        int liveAmmoCount = 0;
        foreach (Ammo ammo in ammoList)
        {
            if (ammo.GetIsLive())
            {
                liveAmmoCount++;
            }
        }
        return liveAmmoCount;
    }

    public int GetBlankAmmoCount()
    {
        int blankAmmoCount = 0;
        foreach (Ammo ammo in ammoList)
        {
            if (!ammo.GetIsLive())
            {
                blankAmmoCount++;
            }
        }
        return blankAmmoCount;
    }

}
