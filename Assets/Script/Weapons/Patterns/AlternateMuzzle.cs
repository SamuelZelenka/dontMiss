using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateMuzzle : Pattern
{
    private int _muzzleIndex = 0;
    public override void Trigger(Projectile projectile, Vector3 muzzlePos, float fireRate)
    {
        throw new System.NotImplementedException();
    }

    public override void Trigger(Projectile projectile, Vector3[] muzzlePos, float fireRate)
    {
        _muzzleIndex = (_muzzleIndex + 1) % muzzlePos.Length;
        Object.Instantiate(projectile, muzzlePos[_muzzleIndex], Quaternion.Euler(Vector3.zero));
    }
}
