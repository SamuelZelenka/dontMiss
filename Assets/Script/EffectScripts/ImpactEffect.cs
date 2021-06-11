using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationEndTrigger))]
public class ImpactEffect : MonoBehaviour
{
    private AnimationEndTrigger _endTrigger;
    private float _initTime;
    private void Awake()
    {
        _endTrigger = GetComponent<AnimationEndTrigger>();
        _endTrigger.OnAnimationEnd += Destroy;
        _initTime = Time.time;
    }
    private void Destroy()
    {
        _endTrigger.OnAnimationEnd -= Destroy;
        Destroy(gameObject);
    }
}


