using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponTemplate : ScriptableObject
{
    public abstract void Shoot(Vector3 muzzles);
    public abstract float GetFireRate();
}
