using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEmAllObjective : MissionObjective
{
    List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    private void Awake()
    {
        enemySpawners.AddRange(GetComponents<EnemySpawner>());
    }
    public override bool IsCompleted()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner.AliveEnemyCount != 0 || spawner.TotalEnemyCount < spawner.MaxEnemyCount)
            {
                return false;
            }
        }
        return true;
    }
}
