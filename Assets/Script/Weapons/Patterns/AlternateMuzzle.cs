using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateMuzzle : Pattern
{
    private int _muzzleIndex = 0;

    public override void Trigger(Projectile projectile, Transform[] muzzlePos, float fireRate, int damage, bool isPlayer)
    {
        _muzzleIndex = (_muzzleIndex + 1) % muzzlePos.Length;
        Projectile newProjectile = Object.Instantiate(projectile, muzzlePos[_muzzleIndex].position, Quaternion.Euler(Vector3.zero)) ;
        newProjectile.Init(10, 2, muzzlePos[_muzzleIndex].rotation, damage, isPlayer);
    }
    public override string ToString()
    {
        return "AlternateMuzzle";
    }
   
}
