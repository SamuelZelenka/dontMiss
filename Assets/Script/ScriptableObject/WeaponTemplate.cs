using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponTemplate : ScriptableObject
{
    public abstract WeaponProjectile Shoot(Vector3 muzzles, bool isPlayer);
    public abstract float GetFireRate();
    public abstract int GetDamage();
}
