using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Alternate Weapon", menuName = "Weapons/Alternate Muzzle")]
public class AlternateMuzzle : WeaponTemplate
{
    [SerializeField] int damage;
    [SerializeField] float lifetime;
    [SerializeField] float fireRate;
    [SerializeField] float speed;

    [SerializeField] WeaponProjectile projectilePrefab;

    public override float GetFireRate() => fireRate;
    public override int GetDamage() => damage;

    public override WeaponProjectile Shoot(Transform muzzle, bool isPlayer)
    {
        WeaponProjectile projectile = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        projectile.Init(lifetime, speed, muzzle.rotation, damage, isPlayer);
        return projectile;
    }
}
