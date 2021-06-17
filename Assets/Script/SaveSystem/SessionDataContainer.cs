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
            return _maxVesselHP;
        }
        set
        {
            if (value < 1)
            {
                _maxVesselHP = 1;
            }
            else
            {
                _maxVesselHP = value;
            }
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public int MaxVesselArmor
    {

        get
        {
            return _maxVesselArmor;
        }
        set
        {
            if (value < 1)
            {
                _maxVesselArmor = 1;
            }
            else
            {
                _maxVesselArmor = value;
            }
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public int VesselHP
    {
        get
        {
            return _vesselHP;
        }
        set
        {
            if (_vesselHP > _maxVesselHP)
            {
                _vesselHP = _maxVesselHP;
            }
            _vesselHP = value;
            GameSession.Instance?.OnStatsChange?.Invoke();
        }
    }
    public int VesselArmor
    {
        get
        {
            return _vesselArmor;
        }
        set
        {
            _vesselArmor = value;
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
    [SerializeField] private int _maxVesselHP;
    [SerializeField] private int _maxVesselArmor;
    [SerializeField] private int _vesselHP;
    [SerializeField] private int _vesselArmor;
    [SerializeField] private int _money;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private MissionProgressionData _missionProgression;
}