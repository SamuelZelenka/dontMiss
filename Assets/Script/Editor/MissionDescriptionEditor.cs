using UnityEngine;
using UnityEditor;

public class MyWindow : EditorWindow
{
    private string _missionName = "New Mission";
    private string _missionDescription = "Enter Mission Description...";


    // Add menu named "My Window" to the Window menu
    [MenuItem("Editor/Mission Description")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Create Mission"))
        {
            SaveSystem.SaveMissionData(_missionName, _missionDescription);
        }
        GUILayout.Label("Mission Creator", EditorStyles.boldLabel);
        _missionName = EditorGUILayout.TextField("Mission Name", _missionName);
        _missionDescription = EditorGUILayout.TextArea(_missionDescription, GUILayout.Height(position.height - 100));
    }
}