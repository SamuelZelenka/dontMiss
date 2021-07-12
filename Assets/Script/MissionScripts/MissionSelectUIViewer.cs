using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSelectUIViewer : MonoBehaviour
{
    [SerializeField] private Text _missionName;
    [SerializeField] private Text _missionDescription;
    [SerializeField] private GameObject _missionWindow;

    public void StartMission() => MissionSelectionManager.Instance.StartSelectedMission();


    private void Start()
    {
        MissionSelectionManager.Instance.OnMissionSelect += UpdateMission;
    }
    private void OnDisable()
    {
        MissionSelectionManager.Instance.OnMissionSelect -= UpdateMission;
    }

    public void UpdateMission(MissionData data)
    {
        _missionWindow.SetActive(true);
        _missionName.text = data.missionName;
        _missionDescription.text = SaveSystem.LoadMissionDescription(data.missionName);
    }
}
