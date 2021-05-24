using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private InputField _nameInput;
    [SerializeField] private Transform _vesselGrid;
    [SerializeField] private VesselLoader _vesselLoaderPrefab;

    [Header("On Selected Vessel")]
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _deleteButton;


    [Header("Vessel Info")]
    [SerializeField] private Text _vesselName;
    [SerializeField] private Text _vesselHP;
    [SerializeField] private Text _vesselArmor;
    [SerializeField] private Text _money;

    private void Awake()
    {
        FindObjectOfType<GameSession>().OnDataLoad += UpdateVesselInfo;
    }

    public void NewGame()
    {
        if (_nameInput.text != "" && _nameInput.text.Length > 3)
        {
            DataContainer newData = new DataContainer();

            newData.vesselName = _nameInput.text;
            newData.vesselHP = 100;
            newData.vesselArmor = 100;
            newData.money = 800;

            SaveSystem.SaveData(newData, newData.vesselName);
            GameSession.Instance.LoadData(newData.vesselName);
            
            UpdateVesselInfo();
        }
    }
    public void DeleteCurrentVessel()
    {
        if (File.Exists(SaveSystem.SAVE_DIRECTORY + GameSession.Instance.sessionData.vesselName + ".rekt"))
        {
            File.Delete(SaveSystem.SAVE_DIRECTORY + GameSession.Instance.sessionData.vesselName + ".rekt");
            GameSession.Instance.sessionData = null;
            UpdateVesselInfo();
            DisplaySavedVessels();
        }
    }
    public void UpdateVesselInfo()
    {
        if (GameSession.Instance.sessionData != null)
        {
            _vesselName.text = GameSession.Instance.sessionData.vesselName;
            _vesselHP.text = GameSession.Instance.sessionData.vesselHP.ToString();
            _vesselArmor.text = GameSession.Instance.sessionData.vesselArmor.ToString();
            _money.text = GameSession.Instance.sessionData.money.ToString();
            _deleteButton.SetActive(true);
            _startButton.SetActive(true);
        }
        else
        {
            _vesselName.text = "";
            _vesselHP.text = "";
            _vesselArmor.text = "";
            _money.text = "";
            _deleteButton.SetActive(false);
            _startButton.SetActive(false);
        }
    }
    public void DisplaySavedVessels()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SaveSystem.SAVE_DIRECTORY);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.rekt");

        Utility.DestroyChildObjects(_vesselGrid);
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
