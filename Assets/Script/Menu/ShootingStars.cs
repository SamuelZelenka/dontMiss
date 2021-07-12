using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStars : MonoBehaviour
{
    float _lastShootTime = 0;
    float _fireRate;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _fireRate = Random.Range(4f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_fireRate + _lastShootTime < Time.time)
        {
            _fireRate = Random.Range(4f, 100);
            _lastShootTime = Time.time;
            _animator.SetTrigger("Trail" + Random.Range(1, 5));
        }
    }

}
