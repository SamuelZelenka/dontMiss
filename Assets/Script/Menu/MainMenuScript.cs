using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private InputField _nameInput;
    [SerializeField] private Transform _vesselGrid;
    [SerializeField] private VesselLoader _vesselLoaderPrefab;

    [Header("Vessel Info")]
    [SerializeField] private Text _vesselName;
    [SerializeField] private Text _vesselHP;
    [SerializeField] private Text _vesselArmor;
    [SerializeField] private Text _money;

    private void Awake()
    {
        FindObjectOfType<GameSession>().OnDataLoad += UpdateVesselInfo;
    }
    private void OnDisable()
    {
        FindObjectOfType<GameSession>().OnDataLoad -= UpdateVesselInfo;
    }

    public void NewGame()
    {
        DataContainer newData = new DataContainer();
        newData.vesselName = _nameInput.text;
        newData.vesselHP = 100;
        newData.vesselArmor = 100;
        newData.money = 800;
        if (newData.vesselName.Length > 3)
        {
            SaveSystem.SaveData(newData, newData.vesselName);
            SaveSystem.LoadData(newData.vesselName);
        }
        UpdateVesselInfo();
    }
    public void UpdateVesselInfo()
    {
        _vesselName.text = GameSession.Instance.sessionData.vesselName;
        _vesselHP.text = GameSession.Instance.sessionData.vesselHP.ToString();
        _vesselArmor.text = GameSession.Instance.sessionData.vesselArmor.ToString();
        _money.text = GameSession.Instance.sessionData.money.ToString();
    }
    public void DisplaySavedVessels()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SaveSystem.SAVE_DIRECTORY);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.rekt");
        
        foreach (FileInfo fileInfo in saveFiles)
        {
            if (fileInfo != null)
            {
                string saveString = File.ReadAllText(fileInfo.FullName);
                VesselLoader vessel = Instantiate(_vesselLoaderPrefab);
                vessel.transform.SetParent(_vesselGrid);

                DataContainer data = JsonUtility.FromJson<DataContainer>(saveString);

                vessel.vesselName.text = data.vesselName;
            }
        }
    }
}
