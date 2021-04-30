using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 targetMovementDirection;
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] WeaponTemplate weapon;
    [SerializeField] Vector3[] muzzlePoints;
    private int muzzleIndex = 0;
    private float lastShotTime = 0;

    // Update is called once per frame
    void Update()
    {
        targetMovementDirection = Vector3.zero;
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            targetMovementDirection += Vector3.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
            targetMovementDirection += Vector3.up * Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > lastShotTime + weapon.GetFireRate()/muzzlePoints.Length)
        {
            lastShotTime = Time.time;
            Shoot();
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + targetMovementDirection, movementSpeed) ;
    }

    private void Shoot()
    {

        muzzleIndex = ((muzzleIndex + 1) % muzzlePoints.Length);
        weapon.Shoot(transform.position + muzzlePoints[muzzleIndex]);
    }
}



