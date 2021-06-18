using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionProgressionData 
{
    [SerializeField] private Vector2Int _playerPos;
    [SerializeField] private List<MissionData> _missions;
    public int Count => _missions.Count;
    public void GenerateNewMissions()
    {
        AddMission(_playerPos + new Vector2Int(0, 1), GameSession.GetRandomMission());
        AddMission(_playerPos + new Vector2Int(1, 0), GameSession.GetRandomMission());
        AddMission(_playerPos + new Vector2Int(0, -1), GameSession.GetRandomMission());
        AddMission(_playerPos + new Vector2Int(-1, 0), GameSession.GetRandomMission());
    }
    public MissionData AddMission(Vector2Int pos, string missionName)
    {
        if (_missions == null)
        {
            _missions = new List<MissionData>();
        }
        _missions.Add(new MissionData(pos, missionName));
        return _missions[_missions.Count - 1];
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
        return null;
    }
    public void SetPlayerPos(Vector2Int pos)
    {
        _playerPos = pos;
    }
    public Vector2Int GetPlayerPos() => _playerPos;
}

[System.Serializable]
public class MissionData
{
    public bool accomplished;
    public Vector2Int position;
    public string missionName;

    public MissionData(Vector2Int pos, string missionName)
    {
        accomplished = false;
        position = pos;
        this.missionName = missionName;
    }
}