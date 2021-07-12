using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUtility.Unity;

public abstract class PlayerMovementState
{
    public virtual void StateUpdate() { }
}

public class OnLocationState : PlayerMovementState
{

    MissionData mission;

    public OnLocationState(MissionData mission)
    {
        this.mission = mission;
        GameSession.Instance.sessionData.MissionProgression.SetPlayerPos(mission.position);
        if (mission.accomplished)
        {
            GameSession.Instance.sessionData.MissionProgression.GenerateNewMissions();
        }
        GameSession.Instance.SaveData();
    }

}
public class TravelingState : PlayerMovementState
{
    int _pathIndex;
    Vector2Int _toPos;
    Vector2Int[] _path;
    MissionSelectPlayer _player;
    MissionGrid _grid;


    public TravelingState(MissionGrid grid, MissionSelectPlayer player, Vector2Int toPos)
    {
        _grid = grid;
        _player = player;
        _path = _grid.GetPathInfoBetween(player.PlayerPos, toPos).path;
        _pathIndex = 0;
        _toPos = toPos;
    }

    public override void StateUpdate()
    {
        Vector3 targetPos = _grid.GetWorldPos(_path[_pathIndex]);

        if (_pathIndex == _path.Length - 1)
        {
            targetPos = _grid.GetWorldPos(_toPos); ;
        }

        if (Vector3.Distance(_player.transform.position, targetPos) < 0.1f)
        {
           
            if (_pathIndex == _path.Length - 1)
            {
                _player.moveState = new OnLocationState(GameSession.Instance.sessionData.MissionProgression.GetMissionAt(_toPos));
                return;
            }
            _pathIndex++;
        }
        _player.transform.position = Vector3.MoveTowards(_player.transform.position, targetPos, _player.travelSpeed * Time.deltaTime);
    }
}


