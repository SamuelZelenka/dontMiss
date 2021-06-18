using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SESSION_SAVE_DIRECTORY = Application.dataPath + "/Saves/";
    public static readonly string MISSION_SAVE_DIRECTORY = Application.dataPath + "/MissionData/";

    #region Session Data
    public static void SaveSessionData(SessionDataContainer data)
    {
        if (!Directory.Exists(SESSION_SAVE_DIRECTORY))
        {
            Directory.CreateDirectory(SESSION_SAVE_DIRECTORY);
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SESSION_SAVE_DIRECTORY + data.VesselName + ".rekt", json);
        Debug.Log("Saved Game Session");
    }

    public static SessionDataContainer LoadSessionData(string saveName) 
    {
        string data = GetSessionDataString(saveName);
        return JsonUtility.FromJson<SessionDataContainer>(data);
    }

    public static string GetSessionDataString(string saveName)
    {
        saveName = SESSION_SAVE_DIRECTORY + saveName + ".rekt";
        if (File.Exists(saveName))
        {
            return File.ReadAllText(saveName);
        }
        return null;
    }
#endregion
    #region Mission Data
    public static void SaveMissionData(string missionName, string missionDescription)
    {
        if (!Directory.Exists(MISSION_SAVE_DIRECTORY))
        {
            Directory.CreateDirectory(MISSION_SAVE_DIRECTORY);
        }
        File.WriteAllText(MISSION_SAVE_DIRECTORY + missionName + ".mission", missionDescription);
        Debug.Log("Saved Mission");
    }
    public static string LoadMissionDescription(string missionName)
    {
        missionName = MISSION_SAVE_DIRECTORY + missionName + ".mission";
        if (File.Exists(missionName))
        {
            return File.ReadAllText(missionName);
        }
        return null;
    }
    #endregion
}
