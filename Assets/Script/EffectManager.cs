using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Dictionary<Ability, int> Abilities = new Dictionary<Ability, int>();
    public delegate void PassiveAbilityHandler();
    public delegate void PositionAbilityHandler(Vector3 position);

    public static PositionAbilityHandler mediumExplosion;
    public static PositionAbilityHandler tinyImpact;

    [SerializeField] public GameObject explosionPrefab;
    [SerializeField] public GameObject TinyImpactPrefab;

    private static EffectManager _instance;

    public static EffectManager Instance { get { return _instance; } }

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
        EffectManager.Instance.Abilities.Add(new ExplosiveBullet(), 1);
    }
}

public abstract class Ability { }


public class ExplosiveBullet : Ability
{
    public ExplosiveBullet()
    {
        EffectManager.mediumExplosion += TriggerExplosion;
        EffectManager.tinyImpact += TriggerTinyImpact;
    }
    public void TriggerExplosion(Vector3 pos)
    {
        Object.Instantiate(EffectManager.Instance.explosionPrefab, pos, Quaternion.Euler(Vector3.zero));
    }
    public void TriggerTinyImpact(Vector3 pos)
    {
        Object.Instantiate(EffectManager.Instance.TinyImpactPrefab, pos, Quaternion.Euler(Vector3.zero));
    }
}