using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_DIRECTORY = Application.dataPath + "/Saves/";
    private const string SAVE_SEPARATOR = "#SAVE-VALUE#";
    public static void SaveData(DataContainer data, string saveName)
    {
        if (!Directory.Exists(SAVE_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DIRECTORY);
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SAVE_DIRECTORY + saveName + ".rekt", json);
        Debug.Log("Saved");
    }
    public static DataContainer LoadData(string saveName) 
    {
        string data = GetDataString(saveName);
        return JsonUtility.FromJson<DataContainer>(data);
    }
    private static string GetDataString(string saveName)
    {
        saveName = SAVE_DIRECTORY + saveName + ".rekt";
        if (File.Exists(saveName))
        {
            return File.ReadAllText(saveName);
        }
        return null;
    }
}
