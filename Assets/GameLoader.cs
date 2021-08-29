using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameLoader : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartLoading());
    }

    IEnumerator StartLoading()
    {
        yield return new WaitForSecondsRealtime(Random.Range(1f,3f));
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);

        SceneManager.sceneLoaded += OnSceneLoaded;   
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (SceneManager.sceneCount == 4)
            SceneManager.UnloadSceneAsync(3);
    }
}
