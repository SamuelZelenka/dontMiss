using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponProjectile : MonoBehaviour
{
    public PlayerController player;
    protected bool Hit;
    protected int Damage;
    protected bool isPlayerProjectile;

    public bool IsPlayerProjectile => isPlayerProjectile;

    public abstract void Init(float lifetime, float speed, Quaternion rotation, int damage, bool isPlayerProjectile);

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
