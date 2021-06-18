using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUtility;

public class MissionSelectPlayer : MonoBehaviour
{
    [SerializeField] private float _travelSpeed = 1f;
    private float _rotationSpeed = 500f;
    [SerializeField] public bool onMission;

    [SerializeField] private MissionGrid _grid;

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
        Vector3 targetPos;
        Vector3 newDirection;
        _grid.PlaceMissionCells();
        if (!onMission)
        {
            MissionData mission = GameSession.Instance.sessionData.MissionProgression.GetMissionAt(GameSession.Instance.sessionData.MissionProgression.GetPlayerPos());
            if (Vector3.Distance(transform.position, targetPos = _grid.GetWorldPos(mission.position)) < 0.1f && !onMission)
            {
                onMission = true;
                GameSession.Instance.currentMission = GameSession.Instance.sessionData.MissionProgression.GetMissionAt(GameSession.Instance.sessionData.MissionProgression.GetPlayerPos());
                if (mission.accomplished)
                {
                    GameSession.Instance.sessionData.MissionProgression.GenerateNewMissions();
                    _grid.PlaceMissionCells();
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _travelSpeed * Time.deltaTime);
        }
    }
}
