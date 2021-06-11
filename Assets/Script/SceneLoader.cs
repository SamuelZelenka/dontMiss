using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameUI", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Background", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        Debug.LogWarning("DEBUG SCENE IS MANUALLY LOADED");
        UnityEngine.SceneManagement.SceneManager.LoadScene("DebugMission", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
