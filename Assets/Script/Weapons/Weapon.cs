using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon
{
    [SerializeField] private float _damageMultiplier;
    [SerializeField] private float _fireRate;
    [SerializeField] private string _pattern;
    [SerializeField] private string _effect;
    [SerializeField] private string _projectile;

    private float lastShotTime;

    public Weapon(float damageMultiplier, float fireRate, string pattern, string effect, string projectile)
    {
        _damageMultiplier = damageMultiplier;
        _fireRate = fireRate;
        _pattern = pattern;
        _effect = effect;
        _projectile = projectile;
    }

    public Pattern Pattern
    {
        get
        {
            return PatternLookup.Get(_pattern);
        }

        set
        {
            _pattern = value.ToString();
        }
    }
    public Projectile Projectile
    {
        get
        {
            return ProjectileLookup.Get(_projectile);
        }

        set
        {
            _projectile = value.ToString();
        }
    }

    public void Fire(Transform[] muzzles, int damage, bool isPlayer)
    {
        if (lastShotTime + _fireRate < Time.time)
        {
            lastShotTime = Time.time;
            Pattern.Trigger(Projectile, muzzles, _fireRate, damage, isPlayer);
        }
    }

}

