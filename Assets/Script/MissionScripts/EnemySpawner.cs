using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<Enemy> _aliveEnemies = new List<Enemy>();
    private int _totalEnemyCount = 0;
    private MissionObjective _missionObjective;
    private bool isActive = false;

    [SerializeField] private float _spawnRate = 2;
    [SerializeField] private float _spawnDelay = 0;
    [SerializeField] private int _maxEnemyCount = 1;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private PathCreator _path;
    [SerializeField] private UIViewer _ui;

    public int AliveEnemyCount => _aliveEnemies.Count;
    public int TotalEnemyCount => _totalEnemyCount;
    public int MaxEnemyCount => _maxEnemyCount;


    private void Awake()
    {
        _ui = FindObjectOfType<UIViewer>();
        _missionObjective = GetComponent<MissionObjective>();
    }
    private void Update()
    {
        if (!isActive && _spawnDelay < Time.timeSinceLevelLoad)
        {
            isActive = true;
            StartCoroutine("SpawnEnemy");
        }
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath -= OnEnemyDeath;       
        _aliveEnemies.Remove(enemy);
        if (_missionObjective.IsCompleted())
        {
            //Set Mission to accomplished
            GameSession.Instance.sessionData.MissionProgression.GetMissionAt(GameSession.Instance.sessionData.MissionProgression.GetPlayerPos()).accomplished = true;
            _ui.EnableWinOverlay();
            GameSession.Instance.sessionData.MissionProgression.GenerateNewMissions();
            GameSession.Instance.SaveData();
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(_spawnRate);
        Enemy enemy = Instantiate(_enemyPrefab, new Vector3(10000,10000,0), _enemyPrefab.transform.rotation);
        enemy.pathCreator = _path;
        _totalEnemyCount++;
        _aliveEnemies.Add(enemy);
        enemy.OnDeath += OnEnemyDeath;

        if (_totalEnemyCount < _maxEnemyCount)
        {
            StartCoroutine("SpawnEnemy");
        }
    }
}
