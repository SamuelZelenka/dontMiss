using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZUtility.Unity;

public class MissionGrid : MonoBehaviour
{
    public const int GRIDSIZE = 50;

    public const float CELLSIZE = 5;

    private ZGrid _grid;

    private Transform _cellParent;

    [SerializeField] MissionSelectionCell _missionPrefab;

    public GridCell[,] GridArray => _grid.gridArray;

    private void Start()
    {
        _grid = new ZGrid(GRIDSIZE, GRIDSIZE, CELLSIZE, Vector3.zero);
        _grid.DrawDebugGrid();

        Vector3 minpos = new Vector3(-CELLSIZE / 2, -CELLSIZE / 2, 0);
        Vector3 maxPos = new Vector3(_grid.gridArray.GetLength(0) * CELLSIZE - CELLSIZE / 2, _grid.gridArray.GetLength(0) * CELLSIZE - CELLSIZE / 2, 0);

        ZUtilityCamera camera = Camera.main.GetComponent<ZUtilityCamera>();
        camera.SetBoundries(minpos, maxPos);
        Vector3 defaultMissionPos = _grid.GetWorldPosition(GameSession.Instance.sessionData.MissionProgression.GetMission(0).position);
        camera.SetPosition(defaultMissionPos);

        _cellParent = new GameObject("MissionSelectionCells").transform;
    }

    public void PlaceMissionCells()
    {
        for (int i = 0; i < GameSession.Instance.sessionData.MissionProgression.Count; i++)
        {
            MissionData data = GameSession.Instance.sessionData.MissionProgression.GetMission(i);

            if (_grid.gridArray[data.position.x, data.position.y].value == null)
            {
                MissionSelectionCell cell = Instantiate(_missionPrefab);
                cell.transform.SetParent(_cellParent);
                cell.Init(data, ref _grid);
                _grid.gridArray[data.position.x, data.position.y].value = data.missionName;
            }
        }
    }
    public Vector3 GetWorldPos(Vector2Int pos) => _grid.GetWorldPosition(pos.x, pos.y);
}
