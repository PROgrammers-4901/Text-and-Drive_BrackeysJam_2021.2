using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private int scenesUnloaded = 0;
    
    public void TryAgain()
    {
        SceneManager.UnloadSceneAsync(1);
        SceneManager.UnloadSceneAsync(2);

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        scenesUnloaded++;

        if (scenesUnloaded == 2)
        {
            GameManager.Instance.StartGame();
            scenesUnloaded = 0;
        }
    }
}


