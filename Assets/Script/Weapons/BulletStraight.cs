using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStraight : WeaponProjectile
{
    private float _speed;
    private float _initTime;
    private float _lifeTime;

    public override void Init(float lifeTime, float speed, int damage, bool isPlayer)
    {
        _speed = speed;
        _lifeTime = lifeTime;
        _initTime = Time.time;
        Hit = false;
        Damage = damage;
        isPlayerProjectile = isPlayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponProjectile projectile = collision.GetComponent<WeaponProjectile>();
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
