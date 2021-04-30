using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ExplosionTemplate template;
    private float initTime;
    private void Awake()
    {
        initTime = Time.time;
    }
    private void Update()
    {
        if (initTime + template.lifetime < Time.time)
        {
            Destroy(gameObject);
        }
    }
}


