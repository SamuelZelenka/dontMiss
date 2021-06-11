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

    public override WeaponProjectile Shoot(Vector3 muzzle , bool isPlayer)
    {
        WeaponProjectile projectile = Instantiate(projectilePrefab, muzzle, Quaternion.Euler(Vector3.zero));
        projectile.Init(lifetime, speed, damage, isPlayer);
        return projectile;
    }
}
