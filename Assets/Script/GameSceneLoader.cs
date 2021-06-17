using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLoader : SceneLoader
{
    public override void Start()
    {
        base.Start();
        SceneManager.LoadScene(GameSession.Instance.currentMission, LoadSceneMode.Additive);
    }
}
