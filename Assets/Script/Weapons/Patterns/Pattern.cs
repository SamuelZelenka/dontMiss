using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern
{

    protected bool isFiring;

    public abstract void Trigger(Projectile projectile, Vector3 pos, float fireRate);
    public abstract void Trigger(Projectile projectile, Vector3[] pos, float fireRate);


}
