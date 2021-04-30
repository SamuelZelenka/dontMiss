using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStraight : WeaponProjectile
{
    float speed;
    float initTime;
    float lifeTime;

    public override void Init(float lifeTime, float speed)
    {
        this.speed = speed;
        this.lifeTime = lifeTime;
        initTime = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerAbilities.explosiveBullet?.Invoke(transform.position);
        Destroy(gameObject);
    }
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (Time.time > initTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
