using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[ExecuteInEditMode]

public class PathEditor : Editor
{
    const float NEW_CONTROL_POINT_SPACING = 1f;
    public static bool enabled;

    private const string MENU_NAME = "Editor/Path Editor";

    #region ToggleMenu
    static PathEditor()
    {
        enabled = EditorPrefs.GetBool(MENU_NAME, true);
    }

    [MenuItem(MENU_NAME)]
    private static void ToggleAction()
    {
        enabled = !enabled;
        EditorPrefs.SetBool(MENU_NAME, enabled);
    }

    [MenuItem(MENU_NAME, true)]
    private static bool ToggleActionValidate()
    {
        Menu.SetChecked(MENU_NAME, enabled);
        return true;
    }
#endregion

    [InitializeOnLoadMethod]
    private static void Init()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }
    private static void OnSceneGUI(SceneView sceneView)
    {
        if (enabled && Event.current.button == 1 && Event.current.type == EventType.MouseUp)
        {
            GenericMenu menu = new GenericMenu();

            // Get clicked position in world space from screen
            Vector3 clickedPos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
            clickedPos.z = 0;

            // Update visuals option is available on all clicks
            menu.AddItem(new GUIContent("Add New Path"), false, NewPath, clickedPos);

            void NewPath(object obj)
            {
                PathCreator path = new GameObject().AddComponent<PathCreator>();
                path.bezierPath.IsClosed = true;
                path.name = "New Path";
            }
            menu.ShowAsContext();
            Event.current.Use();
        }
        UpdateHandles();
    }
    public static void UpdateHandles()
    {
        if (Selection.activeGameObject?.GetComponent<Path>() != null)
        {
            Tools.current = Tool.None;
            Path path = Selection.activeGameObject.GetComponent<Path>();
            for (int i = 0; i < path.controlPoints.Count; i++)
            {
                path.controlPoints[i] = Handles.PositionHandle(path.controlPoints[i], Quaternion.identity);
            }
        }

    }
}
