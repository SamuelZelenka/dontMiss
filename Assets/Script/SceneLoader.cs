using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneLoader : MonoBehaviour
{
    [SerializeField] protected string[] Scenes;
    // Start is called before the first frame update
    public virtual void Start()
    {
        foreach (string sceneName in Scenes)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            if (sceneName.Contains("DebugMission"))
            {
                Debug.LogWarning("DEBUG SCENE IS MANUALLY LOADED");
            }
        }
    }
}
