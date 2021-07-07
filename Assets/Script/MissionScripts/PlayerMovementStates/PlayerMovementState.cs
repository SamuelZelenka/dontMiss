using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUtility.Unity;

public abstract class PlayerMovementState
{
    public abstract void StateUpdate();
}

public class OnLocationState : PlayerMovementState
{
    public override void StateUpdate()
    {
    }
}
public class TravelingState : PlayerMovementState
{
    int _pathIndex;
    ZGridPathInfo _pathInfo;
    MissionSelectPlayer _player;
    MissionGrid _grid;


    public TravelingState(MissionGrid grid, MissionSelectPlayer player, Vector2Int toPos)
    {
        _grid = grid;
        _player = player;
        _pathInfo = _grid.GetPathInfoBetween(player.PlayerPos, toPos);
        _pathIndex = 0;
    }

    public override void StateUpdate()
    {
        Vector3 playerWorldPos = _grid.GetWorldPos(_player.PlayerPos);

        if (Vector3.Distance(_player.transform.position, _grid.GetWorldPos(_pathInfo.path[_pathIndex])) < 0.1f)
        {
            _pathIndex++;
            if (_pathIndex == _pathInfo.path.Length - 1)
            {
                _player.moveState = new OnLocationState();
            }
        }
        _player.transform.position = Vector3.MoveTowards(_player.transform.position, _grid.GetWorldPos(_pathInfo.path[_pathIndex]), _player.travelSpeed);
    }
}
