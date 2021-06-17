using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSession : MonoBehaviour
{
    public delegate void GameDataHandler();
    public delegate void GameSessionHandler();
    public GameSessionHandler OnDataLoad;
    public GameDataHandler OnStatsChange;
    public string currentMission;

    private static GameSession _instance;
    public static GameSession Instance { get { return _instance; } }

    public SessionDataContainer sessionData;

    public void SaveData(string saveName)
    {
        SaveSystem.SaveSessionData(sessionData, saveName);
    }
    public void LoadData(string saveName)
    {
        sessionData = SaveSystem.LoadSessionData(saveName);
        OnDataLoad?.Invoke();
    }
    public static bool DeleteCurrentVessel()
    {
        string path = SaveSystem.SESSION_SAVE_DIRECTORY + _instance.sessionData.VesselName + ".rekt";
        if (File.Exists(path))
        {
            File.Delete(path);
            GameSession.Instance.sessionData = null;
            return true;
        }
        return false;
    }

    private void Awake()
    {
        LoadData(sessionData.VesselName);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}