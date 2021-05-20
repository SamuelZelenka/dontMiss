using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public delegate void GameSessionHandler();
    public GameSessionHandler OnDataLoad;

    List<ISavable> savableObjects = new List<ISavable>();

    private static GameSession _instance;
    public static GameSession Instance { get { return _instance; } }

    public DataContainer sessionData;

    public void SaveData(string saveName)
    {
        SaveSystem.SaveData(sessionData, saveName);
    }
    public void LoadData(string saveName)
    {
        sessionData = SaveSystem.LoadData(saveName);
        OnDataLoad?.Invoke();
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}