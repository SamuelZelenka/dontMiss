using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Enemy : MonoBehaviour, IDamagable
{

    public delegate void EnemyHandler(Enemy enemy);
    public EnemyHandler OnDeath;


    private SpriteRenderer spriteRenderer;

    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public int HP;
    public float speed;
    protected float DstTravelled;

    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        DstTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(DstTravelled, end);
    }
    public virtual void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
            Instantiate(Upgrade.GetRandomUpgradePrefab(), transform.position, Quaternion.Euler(0,0,0));
        }
        spriteRenderer.color = Color.red;
        StartCoroutine(TakeDamageEffect.DamageEffect(spriteRenderer));
    }
}
