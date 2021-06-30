using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUtility;
using ZUtility.Unity;

public class MissionSelectPlayer : MonoBehaviour
{
    [SerializeField] private float _travelSpeed = 1f;
    private float _rotationSpeed = 500f;
    [SerializeField] public bool onMission = false;

    [SerializeField] private MissionGrid _grid;

    private int _pathfindIndex = 0;

    ZGridPathInfo _pathInfo;

    public Vector2Int PlayerPos => GameSession.Instance.sessionData.MissionProgression.GetPlayerPos();

    // Start is called before the first frame update
    void Start()
    {
        MissionSelectionManager.Instance.player = this;
        transform.position = _grid.GetWorldPos(GameSession.Instance.sessionData.MissionProgression.GetPlayerPos());
    }

    // Update is called once per frame
    void Update()
    {
        _grid.PlaceMissionCells();






        if (!onMission)
        {
            MissionData mission = GameSession.Instance.sessionData.MissionProgression.GetMissionAt(GameSession.Instance.sessionData.MissionProgression.GetPlayerPos());

            Vector3 targetPos = _pathInfo != null ? _grid.GetWorldPos(_pathInfo.path[_pathfindIndex]) : _grid.GetWorldPos(PlayerPos); 

            if (Vector3.Distance(transform.position, targetPos) < 0.1f && !onMission)
            {
                if (_pathInfo != null)
                {
                    if (_pathfindIndex == _pathInfo.path.Length - 1)
                    {
                        onMission = true;
                        GameSession.Instance.currentMission = GameSession.Instance.sessionData.MissionProgression.GetMissionAt(GameSession.Instance.sessionData.MissionProgression.GetPlayerPos());
                        GameSession.Instance.sessionData.MissionProgression.SetPlayerPos(_pathInfo.path[_pathfindIndex]);
                        if (mission.accomplished)
                        {
                            GameSession.Instance.sessionData.MissionProgression.GenerateNewMissions();
                            _grid.PlaceMissionCells();
                        }
                    }
                    else
                    {
                        _pathfindIndex++;
                    }
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _travelSpeed * Time.deltaTime);
        }
    }

    public void SetPathTo(Vector2Int pos)
    {
        _pathfindIndex = 0;
        _pathInfo = _grid.GetPathInfoBetween(pos, PlayerPos);
        onMission = false;
    }
}
