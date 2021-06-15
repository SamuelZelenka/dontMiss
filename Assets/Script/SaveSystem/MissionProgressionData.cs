using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionProgressionData 
{
    private List<MissionData> _missions;
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
}

[System.Serializable]
public class MissionData
{
    public bool visited;
    public Vector2Int position;
    public string missionName;

    public MissionData(Vector2Int pos, string missionName)
    {
        visited = false;
        position = pos;
        missionName = missionName;
    }

}