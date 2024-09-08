using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreShield : Modifier
{
    private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        GameObject weaponObject = GameObject.FindGameObjectWithTag("Weapon");
        weapon = weaponObject.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Apply()
    {
        weapon.ignoreShield = true;
    }
}
