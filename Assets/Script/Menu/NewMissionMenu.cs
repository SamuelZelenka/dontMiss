using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NewMissionMenu : MonoBehaviour
{
    [SerializeField] InputField field;
    [SerializeField] Transform grid;
    [SerializeField] VesselLoader vesselLoaderPrefab;
    public void NewGame()
    {
        DataContainer newData = new DataContainer();
        newData.vesselName = field.text;
        if (field.text.Length > 3)
        {
            SaveSystem.SaveData(newData, newData.vesselName);
            SaveSystem.LoadData(field.text);
            gameObject.SetActive(false);
        }
    }
    public void DisplaySavedVessels()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SaveSystem.SAVE_DIRECTORY);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.rekt");
        
        foreach (FileInfo fileInfo in saveFiles)
        {
            if (fileInfo != null)
            {
                string saveString = File.ReadAllText(fileInfo.FullName);
                VesselLoader vessel = Instantiate(vesselLoaderPrefab);
                vessel.transform.SetParent(grid);

                DataContainer data = JsonUtility.FromJson<DataContainer>(saveString);

                vessel.vesselName.text = data.vesselName;
            }
        }
    }
}
