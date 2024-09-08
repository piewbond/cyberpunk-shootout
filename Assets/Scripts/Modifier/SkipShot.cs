using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipShot : Modifier
{
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
        GameObject weaponObject = GameObject.FindGameObjectWithTag("Weapon");
        Weapon weapon = weaponObject.GetComponent<Weapon>();
        weapon.SkipShot();
    }
}
