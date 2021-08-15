using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDownShooter : Enemy
{
    private int _muzzleIndex;
    private float _lastShotTime = 0;

    [SerializeField] private float _fireRate;
    [SerializeField] private Transform[] _muzzles;

    [Header("Bullet Values")]
    [SerializeField] private float _bulletLifetime;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _bulletDamage;



    public override void Update()
    {
        base.Update();
        if (Time.time > _lastShotTime + _fireRate)
        {
            Shoot();
            _lastShotTime = Time.time;
        }
    }
    private void Shoot()
    {
        _muzzleIndex++;
        _muzzleIndex = _muzzleIndex % _muzzles.Length;
    }
}
