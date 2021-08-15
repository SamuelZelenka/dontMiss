using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Projectile
{
    private float _speed;
    private float _initTime;
    private float _lifeTime;

    [SerializeField] private ContactFilter2D _shooterFilter;

    public override void Init(float lifeTime, float speed, Quaternion rotation, int damage, bool isPlayer)
    {
        _speed = speed;
        _lifeTime = lifeTime;
        _initTime = Time.time;
        Hit = false;
        Damage = damage;
        isPlayerProjectile = isPlayer;
        transform.rotation = rotation;
        if (isPlayer)
        {
            gameObject.layer = 7;
        }
        else
        {
            gameObject.layer = 6;

        }
        _shooterFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        _shooterFilter.useLayerMask = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null && projectile.IsPlayerProjectile != IsPlayerProjectile)
        {
            Destroy(gameObject);
            EffectManager.tinyImpact?.Invoke(transform.position); //spawn explosion effect
            return;
        }
        IDamagable damagable = collision.transform.GetComponent<IDamagable>();
        if (damagable != null)
        {
            if ((damagable.GetType() == typeof(PlayerController) && isPlayerProjectile) ||
                (damagable.GetType() != typeof(PlayerController) && !isPlayerProjectile))
            {
                return;
            }

            EffectManager.mediumExplosion?.Invoke(transform.position); //spawn explosion effect
            Hit = true;
            damagable.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
        if (Time.time > _initTime + _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
