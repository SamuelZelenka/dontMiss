using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SessionDataContainer
{
    public bool IsDebugMode => _isDebugMode;
    public string VesselName 
    { 
        get 
        {
            return _vesselName;
        } 
        set 
        {
            _vesselName = value;
        } 
    }
    public int MaxVesselHP
    {
        get
        {
            return _maxBaseHP + GetAdditionalHP();
        }
        set
        {
            if (value < 1)
            {
                _maxBaseHP = 1;
            }
            else
            {
                _maxBaseHP = value;
            }
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public int MaxAdditionalHP
    {
        get
        {
            return _maxAdditionalHP;
        }
        set
        {
            _maxAdditionalHP = value;
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public int MaxVesselArmor
    {

        get
        {
            return _maxBaseArmor + _maxAdditionalArmor;
        }
        set
        {
            if (value < 1)
            {
                _maxBaseArmor = 1;
            }
            else
            {
                _maxBaseArmor = value;
            }
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public int MaxAdditionalArmor
    {
        get
        {
            return _maxAdditionalArmor;
        }
        set
        {
            _maxAdditionalArmor = value;
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public int VesselHP
    {
        get
        {
            return _currentHP;
        }
        set
        {
            if (_currentHP > _maxBaseHP)
            {
                _currentHP = _maxBaseHP;
            }
            _currentHP = value;
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public int VesselArmor
    {
        get
        {
            return _currentArmor;
        }
        set
        {
            _currentArmor = value;
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public float MovementSpeed
    {
        get
        {
            return _movementSpeed;
        }
        set
        {
            _movementSpeed = value;
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public void AddUpgrade(IUpgradable upgrade)
    {
        _upgrades.Add(upgrade.ToString());
        ReferenceLibrary.Instance.GetUpgrade(_upgrades[_upgrades.Count - 1]).ApplyUpgrade(this);
        
    }
    public List<string> GetUpgrades() => _upgrades;
    private int GetAdditionalHP()
    {
        int additionalHP = 0;
        foreach (string upgrade in _upgrades)
        {
            if (ReferenceLibrary.Instance.GetUpgrade(upgrade).GetType() == typeof(HealthUpgrade))
            {
                HealthUpgrade healthUpgrade = ReferenceLibrary.Instance.GetUpgrade(upgrade) as HealthUpgrade;
                additionalHP += healthUpgrade.amount;
            }
        }
        return additionalHP;
    }
    public MissionProgressionData MissionProgression
    {
        get
        {
            return _missionProgression;
        }
        set
        {
            _missionProgression = value;
        }
    }

    [SerializeField] private bool _isDebugMode = false;
    [SerializeField] private string _vesselName;
    [SerializeField] private int _maxBaseHP;
    [SerializeField] private int _maxBaseArmor;
   
    [SerializeField] private int _currentHP;
    [SerializeField] private int _currentArmor;
    [SerializeField] private int _money;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private List<string> _upgrades;
    [SerializeField] private MissionProgressionData _missionProgression;

    private int _maxAdditionalHP;
    private int _maxAdditionalArmor;
}