using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    private Vector3 _minMovementPos;
    private Vector3 _maxMovementPos;
    private Vector3 _targetMovementDirection;
    private Vector3 _targetMovementPosition;

    private int _muzzleIndex = 0;
    private float _lastShotTime = 0;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private WeaponTemplate _weapon;
    [SerializeField] private Vector3[] _muzzlePoints;

    [SerializeField] private UIViewer _ui;


    private SessionDataContainer Data => GameSession.Instance.sessionData;
    private void Awake()
    {
        _ui = FindObjectOfType<UIViewer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _minMovementPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _maxMovementPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
    }
    void Update()
    {
        _targetMovementDirection = Vector3.zero;
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _targetMovementDirection += Vector3.right * Input.GetAxis("Horizontal") * Data.MovementSpeed * Time.deltaTime;
            _targetMovementDirection += Vector3.up * Input.GetAxis("Vertical") * Data.MovementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > _lastShotTime + _weapon.GetFireRate() / _muzzlePoints.Length)
        {
            _lastShotTime = Time.time;
            Shoot();
        }
        ClampPosition();
        transform.position = Vector3.MoveTowards(transform.position, _targetMovementPosition, Data.MovementSpeed) ;

        if (Input.GetKeyDown(KeyCode.H))
        {
            GameSession.Instance.SaveData(Data.VesselName);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameSession.Instance.LoadData(Data.VesselName);
        }
    }
    public void TakeDamage(int damage)
    {
        if (GameSession.Instance.sessionData.VesselHP <= 1)
        {
            _ui.EnableDeathOverlay();
            EffectManager.mediumExplosion?.Invoke(transform.position);

            //Do not delete debug files even if death occurs
            if (!GameSession.Instance.sessionData.IsDebugMode)
            {
                GameSession.DeleteCurrentVessel();
            }
            Destroy(gameObject);
            return;
        }
        GameSession.Instance.sessionData.VesselHP -= damage;
        _spriteRenderer.color = Color.red;
        StartCoroutine(TakeDamageEffect.DamageEffect(_spriteRenderer));
    }
    private void Shoot()
    {
        _muzzleIndex = ((_muzzleIndex + 1) % _muzzlePoints.Length);
        _weapon.Shoot(transform.position + _muzzlePoints[_muzzleIndex], true).player = this;
    }
    private void ClampPosition()
    {
        Rect cameraRect = new Rect(
            _minMovementPos.x,
            _minMovementPos.y,
            _maxMovementPos.x - _minMovementPos.x,
            _maxMovementPos.y - _minMovementPos.y
            );

        _targetMovementPosition.x = Mathf.Clamp(transform.position.x + _targetMovementDirection.x , cameraRect.xMin, cameraRect.xMax);
        _targetMovementPosition.y = Mathf.Clamp(transform.position.y + _targetMovementDirection.y, cameraRect.yMin, cameraRect.yMax);
    }
    public void SetUi(UIViewer ui) => _ui = ui;
}



