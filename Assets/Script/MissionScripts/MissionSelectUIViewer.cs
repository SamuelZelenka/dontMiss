using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSelectUIViewer : MonoBehaviour
{
    [SerializeField] private Text _missionName;
    [SerializeField] private Text _missionDescription;

    public void StartMission() => MissionSelectionManager.Instance.StartSelectedMission();

    private void OnEnable()
    {
        MissionSelectionManager.Instance.OnMissionSelect += UpdateMission;
    }
    private void OnDisable()
    {
        MissionSelectionManager.Instance.OnMissionSelect -= UpdateMission;
    }

    public void UpdateMission(MissionData data)
    {
        _missionName.text = data.missionName;
        _missionDescription.text = SaveSystem.LoadMissionDescription(data.missionName);
    }
}
