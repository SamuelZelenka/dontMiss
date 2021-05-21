using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISavable
{
    private Vector3 targetMovementDirection;
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] WeaponTemplate weapon;
    [SerializeField] Vector3[] muzzlePoints;
    private int muzzleIndex = 0;
    private float lastShotTime = 0;
    private void Start()
    {
        GameSession.Instance.OnDataLoad += SetDataValues;
    }
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

        if (Input.GetKeyDown(KeyCode.H))
        {
            GameSession.Instance.SaveData(GameSession.Instance.sessionData.vesselName);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameSession.Instance.LoadData(GameSession.Instance.sessionData.vesselName);
        }
    }
    public void GetDataValues()
    {
        GameSession.Instance.sessionData.position = transform.position;
    }
    public void SetDataValues()
    {
        transform.position = GameSession.Instance.sessionData.position;
    }
    private void Shoot()
    {

        muzzleIndex = ((muzzleIndex + 1) % muzzlePoints.Length);
        weapon.Shoot(transform.position + muzzlePoints[muzzleIndex]);
    }
}



