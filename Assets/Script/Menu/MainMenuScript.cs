using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private GameSession currentSession;

    [SerializeField] private VesselLoader _vesselLoaderPrefab;

    [SerializeField] private FlexibleLayoutGroup _layout;
    [SerializeField] private InputField _nameInput;

    [Header("On Selected Vessel")]
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _deleteButton;


    [Header("Vessel Info")]
    [SerializeField] private Transform _vesselInfoTransform;
    [SerializeField] private Text _vesselName;
    [SerializeField] private Text _vesselHP;
    [SerializeField] private Text _vesselArmor;
    [SerializeField] private Text _money;



    private void Awake()
    {
        currentSession = FindObjectOfType<GameSession>();
        currentSession.OnDataLoad += UpdateVesselInfo;
    }
    private void OnDisable()
    {
        currentSession.OnDataLoad -= UpdateVesselInfo;
    }

    public void StartGame()
    {
        if (GameSession.Instance.sessionData.VesselName != null || GameSession.Instance.sessionData.VesselName != "")
        {
            SceneManager.LoadScene("MissionSelect", LoadSceneMode.Single);
        }
    }

    //Set Default values
    public void NewGame()
    {
        if (_nameInput.text != "" && _nameInput.text.Length > 3)
        {
            SessionDataContainer newData = new SessionDataContainer();

            newData.MaxVesselHP = 100;
            newData.MaxVesselArmor = 100;
            newData.VesselName = _nameInput.text;
            newData.VesselHP = 100;
            newData.VesselArmor = 100;
            newData.Money = 800;
            newData.MovementSpeed = 3;
            newData.Damage = 1;
          
            newData.MissionProgression = new MissionProgressionData();
            newData.MissionProgression.AddMission(new Vector2Int(MissionGrid.GRIDSIZE / 2, MissionGrid.GRIDSIZE / 2), "DebugMission");

            newData.MissionProgression.SetPlayerPos(newData.MissionProgression.GetMission(0).position);

            newData.Weapon = new Weapon(1, 0.08f, new AlternateMuzzle().ToString(), "", new DefaultBullet().ToString() );

            SaveSystem.SaveSessionData(newData);
            GameSession.Instance.LoadData(newData.VesselName);
            
            UpdateVesselInfo();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void DeleteVesselButton()
    {
        if (GameSession.DeleteCurrentVessel())
        {
            UpdateVesselInfo();
            DisplaySavedVessels();
        }
    }

    public void UpdateVesselInfo()
    {
        if (GameSession.Instance.sessionData != null)
        {
            _vesselName.text = GameSession.Instance.sessionData.VesselName;
            _vesselHP.text = "Health: " + GameSession.Instance.sessionData.VesselHP.ToString();
            _vesselArmor.text = "Armor: " + GameSession.Instance.sessionData.VesselArmor.ToString();
            _money.text = "$" + GameSession.Instance.sessionData.Money.ToString();
            _vesselInfoTransform.gameObject.SetActive(true);
        }
        else
        {
            _vesselInfoTransform.gameObject.SetActive(false);
        }
    }
    public void DisplaySavedVessels()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SaveSystem.SESSION_SAVE_DIRECTORY);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.rekt");

        Utility.DestroyChildObjects(_layout.transform);
        foreach (FileInfo fileInfo in saveFiles)
        {
            if (fileInfo != null)
            {
                string saveString = File.ReadAllText(fileInfo.FullName);
                VesselLoader vessel = Instantiate(_vesselLoaderPrefab, _layout.transform);

                SessionDataContainer data = JsonUtility.FromJson<SessionDataContainer>(saveString);

                vessel.vesselName.text = data.VesselName;
            }
        }
    }
}
