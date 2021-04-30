using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public Dictionary<Ability, int> Abilities = new Dictionary<Ability, int>();
    public delegate void PassiveAbilityHandler();
    public delegate void PositionAbilityHandler(Vector3 position);

    public static PositionAbilityHandler explosiveBullet;


    [SerializeField] public GameObject explosionPrefab;

    private static PlayerAbilities _instance;

    public static PlayerAbilities Instance { get { return _instance; } }

    public static void AddAbility()
    {

    }

    private void Awake()
    {
        

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        PlayerAbilities.Instance.Abilities.Add(new ExplosiveBullet(), 1);
    }
}

public abstract class Ability { }


public class ExplosiveBullet : Ability
{
    public ExplosiveBullet()
    {
        PlayerAbilities.explosiveBullet += Trigger;
    }
    public void Trigger(Vector3 pos)
    {
        GameObject.Instantiate( PlayerAbilities.Instance.explosionPrefab, pos, Quaternion.Euler(Vector3.zero));
    }
}