using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBomber : Enemy
{
    const float DAMAGE_EFFECT_DURATION = 2f;

    private float _lastDropTime = 0;

    [SerializeField] private float _dropRate = 2f;
    [SerializeField] private GameObject _bombPrefab;

    public override void Update()
    {
        base.Update();
        transform.rotation = pathCreator.path.GetRotationAtDistance(DstTravelled, end) * Quaternion.Euler(0, 90, 90);
        if (Time.time > _lastDropTime + _dropRate)
        {
            _lastDropTime = Time.time;
            Instantiate(_bombPrefab, transform.position, Quaternion.Euler(0,0,0)).transform.SetParent(null); 
        }
    }
}
