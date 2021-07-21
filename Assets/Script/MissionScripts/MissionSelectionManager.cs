using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSelectionManager : MonoBehaviour
{

    public delegate void MissionHandler(MissionData mission);
    public MissionHandler OnMissionSelect;

    public MissionSelectPlayer player;

    private MissionData _selectedMission;
    private static MissionSelectionManager _instance;
    public static MissionSelectionManager Instance => _instance;

    public void StartSelectedMission()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void SelectMission(MissionData mission)
    {
        OnMissionSelect?.Invoke(mission);
        _selectedMission = mission;
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
