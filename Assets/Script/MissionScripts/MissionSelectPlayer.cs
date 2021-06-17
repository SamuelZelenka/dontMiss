using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelectPlayer : MonoBehaviour
{
    [SerializeField] float _travelSpeed = 1f;
    float _rotationSpeed = 500f;
    [SerializeField] bool _onMission;

    [SerializeField] private MissionGrid _grid;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = _grid.GetWorldPos(GameSession.Instance.sessionData.MissionProgression.GetPlayerPos());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos;
        Vector3 newDirection;

        if (Vector3.Distance(transform.position, targetPos = _grid.GetWorldPos(GameSession.Instance.sessionData.MissionProgression.GetPlayerPos())) < 0.1f)
        {
            _onMission = true;
            GameSession.Instance.currentMission = GameSession.Instance.sessionData.MissionProgression.GetMissionAt(GameSession.Instance.sessionData.MissionProgression.GetPlayerPos()).missionName;
        }
        else
        {
            _onMission = false;
        }
        
        if (!_onMission)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _travelSpeed * Time.deltaTime);
        }
    }
}
