using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationEndTrigger))]
public class ScatterBomb : MonoBehaviour
{
    private const float SCATTER_OFFSET = 1f;

    private int animationTickCount = 0;
    private int animationTickMax = 5;
    private AnimationEndTrigger endTrigger;

    [SerializeField] private ImpactEffect explosionPrefab;

    [Header("Scatter values")]
    [SerializeField] private float scatterLifetime = 5;
    [SerializeField] private float scatterSpeed = 2;
    [SerializeField] private int scatterDamage = 1;
    [SerializeField] private int scatterCount = 15;
    [SerializeField] private BulletStraight scatterParticle;

    // Start is called before the first frame update
    void Start()
    {
        endTrigger = GetComponent<AnimationEndTrigger>();
        endTrigger.OnAnimationEnd += BombTick;
    }

    void BombTick()
    {
        if (animationTickCount < animationTickMax)
        {
            animationTickCount++;
            return;
        }
        Explode();
    }

    void Explode()
    {
        float angle = 360.0f / scatterCount * Mathf.PI / 180.0f;

        for (int i = 0; i < scatterCount; i++)
        {
            float x;
            float y;

            x = transform.position.x + Mathf.Cos(angle * i) * SCATTER_OFFSET;
            y = transform.position.y + Mathf.Sin(angle * i) * SCATTER_OFFSET;

            WeaponProjectile projectile = Instantiate(scatterParticle);

            Vector3 newPos = new Vector3(x, y, 0);
            projectile.transform.position = newPos;


            Vector2 direction = projectile.transform.position - transform.position;
            float newAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            
            projectile.Init(scatterLifetime, scatterSpeed, projectile.transform.rotation, scatterDamage, false);
        }
        Instantiate(explosionPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }
}
