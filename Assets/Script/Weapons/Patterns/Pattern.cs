using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern
{

    protected bool isFiring;
    public abstract void Trigger(Projectile projectile, Transform[] pos, float fireRate, int damage, bool isPlayer);
}
