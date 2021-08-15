using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceLibrary : MonoBehaviour
{
    //Change to dictionary with Odin inspector when license is here.
    [SerializeField] private Sprite _healthUpgradeSprite;
    [SerializeField] private Sprite _speedUpgradeSprite;
  

    private List<string> _availableUpgrades = new List<string>();
    private Dictionary<string, Upgradable> _upgradeDictionary = new Dictionary<string, Upgradable>();
    public string[] allMissions;
    public List<UpgradePickUp> upgradePrefabs;
    public List<Projectile> projectilePrefabs;

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
    public Upgradable GetUpgrade(string upgradeName) => _upgradeDictionary[upgradeName];
    public UpgradePickUp GetUpgradePrefab(string upgradeName)
    {
        foreach (UpgradePickUp upgrade in upgradePrefabs)
        {
            if (upgrade.upgradeName == upgradeName)
            {
                return upgrade;
            }
        }
        Debug.LogError($"{upgradeName} does not exist in available upgrade prefabs.");
        return null;
    }
    public Projectile GetProjectilePrefab(string projectileName)
    {
        foreach (Projectile projectile in projectilePrefabs)
        {
            if (projectile.ToString() == projectileName)
            {
                return projectile;
            }
        }
        Debug.LogError($"{projectileName} does not exist in available upgrade prefabs.");
        return null;
    }


    public void LoadUpgrade(Upgradable upgrade)
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
