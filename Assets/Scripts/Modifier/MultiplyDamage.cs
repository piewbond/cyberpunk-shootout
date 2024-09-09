using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyDamage : Modifier
{
    [SerializeField]
    public int damageAmount;
    private Weapon weapon;

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
        weapon = weaponObject.GetComponent<Weapon>();
        weapon.MultiplyDamage(damageAmount);
    }

    public override ModifierType GetModifierType()
    {
        return ModifierType.MultiplyDamage;
    }
}
