using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZUtility.Unity;

public class MissionGrid : MonoBehaviour
{
    public const int GRIDSIZE = 50;

    public const float CELLSIZE = 30;

    private ZGrid _grid;
    [SerializeField] MissionSelectionCell _missionPrefab;

    private void Start()
    {
        _grid = new ZGrid(GRIDSIZE, GRIDSIZE, CELLSIZE, Vector3.zero);
        _grid.DrawDebugGrid();

        Vector3 minpos = new Vector3(-CELLSIZE / 2, -CELLSIZE / 2, 0);
        Vector3 maxPos = new Vector3(_grid.gridArray.GetLength(0) * CELLSIZE - CELLSIZE / 2, _grid.gridArray.GetLength(0) * CELLSIZE - CELLSIZE / 2, 0);

        Camera.main.GetComponent<ZUtilityCamera>().SetBoundries(minpos, maxPos);

        MissionSelectionCell cell;

        for (int i = 0; i < GameSession.Instance.sessionData.Missions.Count; i++)
        {
            cell = 
            cell = Instantiate(_missionPrefab);
            cell.Init(GameSession.Instance.sessionData.Missions.GetMission(i), ref _grid);
        }
        
    }
}
