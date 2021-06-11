using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<Enemy> _aliveEnemies = new List<Enemy>();
    private int _totalEnemyCount = 0;
    private MissionObjective _missionObjective;

    [SerializeField] private float _spawnRate = 2;
    [SerializeField] private int _maxEnemyCount = 1;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private PathCreator _path;

    public int AliveEnemyCount => _aliveEnemies.Count;
    public int TotalEnemyCount => _totalEnemyCount;
    public int MaxEnemyCount => _maxEnemyCount;


    private void Awake()
    {
        _missionObjective = GetComponent<MissionObjective>();
        StartCoroutine("SpawnEnemy");
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath -= OnEnemyDeath;       
        _aliveEnemies.Remove(enemy);
        if (_missionObjective.IsCompleted())
        {
            UIViewer.EnableWinOverlay();
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(_spawnRate);
        Enemy enemy = Instantiate(_enemyPrefab);
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
