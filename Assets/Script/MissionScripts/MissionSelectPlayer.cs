using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUtility;
using ZUtility.Unity;

public class MissionSelectPlayer : MonoBehaviour
{
    [SerializeField] private MissionGrid _grid;

    public PlayerMovementState moveState;

    public float travelSpeed = 1f;
    public Vector2Int PlayerPos => GameSession.Instance.sessionData.MissionProgression.GetPlayerPos();

    // Start is called before the first frame update
    void Start()
    {
        moveState = new OnLocationState(GameSession.Instance.sessionData.MissionProgression.GetMissionAt(PlayerPos));
        MissionSelectionManager.Instance.player = this;
        transform.position = _grid.GetWorldPos(PlayerPos);
    }

    // Update is called once per frame
    void Update()
    {
        _grid.PlaceMissionCells();
        moveState.StateUpdate();
    }

    public void SetPathTo(Vector2Int pos)
    {
        moveState = new TravelingState(_grid, this, pos);
    }
}
