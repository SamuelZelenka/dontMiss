using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionProgressionData 
{
    [SerializeField] private Vector2Int _playerPos;
    [SerializeField] private List<MissionData> _missions;
    public int Count => _missions.Count;
    public void AddMission(Vector2Int pos, string missionName)
    {
        if (_missions == null)
        {
            _missions = new List<MissionData>();
        }
        _missions.Add(new MissionData(pos, missionName));
    }
    public MissionData GetMission(int index) => _missions[index];
    public MissionData GetMissionAt(Vector2Int pos)
    {
        foreach (MissionData mission in _missions)
        {
            if (mission.position == pos)
            {
                return mission;
            }
        }
        return new MissionData();
    }
    public void SetPlayerPos(Vector2Int pos)
    {
        _playerPos = pos;
    }
    public Vector2Int GetPlayerPos() => _playerPos;
}

[System.Serializable]
public struct MissionData
{
    public bool visited;
    public Vector2Int position;
    public string missionName;

    public MissionData(Vector2Int pos, string missionName)
    {
        visited = false;
        position = pos;
        this.missionName = missionName;
    }
}