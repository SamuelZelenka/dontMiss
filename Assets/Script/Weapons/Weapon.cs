using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon
{
    [SerializeField] private string _name;
    [SerializeField] private float _damageMultiplier;
    [SerializeField] private float _fireRate;
    [SerializeField] private string _pattern;
    [SerializeField] private string _effect;
    [SerializeField] private string _projectile;

    //public Pattern Pattern 
    //{
    //    get
    //    {
    //        return PatternLookup.Get(_pattern);
    //    }
        
    //    set
    //    {
    //        _pattern = value.ToString();
    //    }
    //}

    public void Fire()
    {

    }

}

