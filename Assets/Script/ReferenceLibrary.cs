using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceLibrary : MonoBehaviour
{
    //Change to dictionary with Odin inspector when license is here.
    [SerializeField] private Sprite _healthUpgradeSprite;
    [SerializeField] private Sprite _speedUpgradeSprite;

    private List<string> _availableUpgrades = new List<string>();
    private Dictionary<string, IUpgradable> _upgradeDictionary = new Dictionary<string, IUpgradable>();
    public string[] allMissions;

    private static ReferenceLibrary _instance;
    public static ReferenceLibrary Instance => _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            LoadAllUpgrades();
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public IUpgradable GetUpgrade(string upgradeName) => _upgradeDictionary[upgradeName];

    public void LoadUpgrade(IUpgradable upgrade)
    {
        _availableUpgrades.Add(upgrade.ToString());
        _upgradeDictionary.Add(upgrade.ToString(), upgrade);
    }
    //Add Upgrades here to include them in game.
    public void LoadAllUpgrades()
    {
        LoadUpgrade(new HealthUpgrade(_healthUpgradeSprite));
        LoadUpgrade(new SpeedUpgrade(_speedUpgradeSprite));
    }
}
