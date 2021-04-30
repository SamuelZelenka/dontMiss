using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponProjectile : MonoBehaviour
{
    public abstract void Init(float lifetime, float speed);
}
