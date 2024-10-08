using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private List<Ammo> ammoList;
    public int ammoCount;
    [SerializeField]
    public int damage;
    public bool ignoreShield;
    [SerializeField]
    public List<Player> players;
    private Player targetPlayer;
    private Player shooterPlayer;
    private bool isLastSelfShot = false;
    [SerializeField]
    MagazineController magazineController;

    public void Shoot(bool shootEnemy)
    {
        isLastSelfShot = false;

        if (players[0].IsActivePlayer())
        {
            shooterPlayer = players[0];
            targetPlayer = players[1];
        }
        else
        {
            shooterPlayer = players[1];
            targetPlayer = players[0];
        }

        if (ammoCount > 0)
            ammoCount--;
        if (ammoList[0].GetIsLive())
        {
            Debug.Log("Target player: " + targetPlayer.playerName + "\nShooter player: " + shooterPlayer.playerName + "Is live: " + ammoList[0].GetIsLive());
            if (shootEnemy)
            {
                targetPlayer.TakeDamage(damage, ignoreShield);
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
            //TODO: Do whiff animation here
            if (!shootEnemy)
                isLastSelfShot = true;
        }
        Debug.Log("Weapon.Shoot     Ammo: " + ammoCount);
        Debug.Log("Live Ammo Count: " + GetLiveAmmoCount());
        Debug.Log("Blank Ammo Count: " + GetBlankAmmoCount());
        damage = 1;
    }

    public void LoadWeapon(int ammoCount)
    {
        //TODO: Implement load weapon animation
        this.ammoCount = ammoCount;
        ammoList = new List<Ammo>();
        for (int i = 0; i < ammoCount; i++)
        {
            Ammo ammo = new Ammo();
            ammoList.Add(ammo);
        }

        for (int i = 0; i < 3; i++)
        {
            int randomCount = Random.Range(0, ammoList.Count);
            ammoList[randomCount].SetIsLive(true);
        }

        magazineController.ShowBullets(GetLiveAmmoCount(), GetBlankAmmoCount());

    }

    public void MultiplyDamage(int damageMultiplier)
    {
        damage *= damageMultiplier;
    }

    public bool IsLastSelfShot()
    {
        return isLastSelfShot;
    }

    public void SkipShot()
    {
        //TODO: Implement skipshot animation
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

    public void InverzBullet()
    {
        if (ammoList.Count > 0)
        {
            ammoList[0].SetIsLive(!ammoList[0].GetIsLive());
        }
    }

}
