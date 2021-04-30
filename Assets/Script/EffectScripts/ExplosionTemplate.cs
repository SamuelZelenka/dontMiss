using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion", menuName = "Effects/Explosion")]
public class ExplosionTemplate : ScriptableObject
{
    public float lifetime;

    public float size;
    public int damage;
}