using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<Ammo> ammoList;
    public int ammoCount;
    // Start is called before the first frame update
    private int damage;

    [SerializeField]
    public List<Player> players;
    private Player targetPlayer;
    private Player shooterPlayer;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(bool shootEnemy)
    {
        if (ammoCount > 0)
        {
            ammoCount--;
            if (ammoList[0].GetIsLive())
            {
                if (shootEnemy)
                {
                    targetPlayer.TakeDamage(damage);
                    SwitchPlayerRole();
                }
                else
                {
                    shooterPlayer.TakeDamage(damage);
                }
                ammoList.RemoveAt(0);
            } 
            else
            {
                Debug.Log("Ammo is not live");
                //Do whiff animation here
            }

            Debug.Log("Shoot");
        }
    }

    private void SwitchPlayerRole()
    {
        Player temp = targetPlayer;
        targetPlayer = shooterPlayer;
        shooterPlayer = temp;
    }

}
