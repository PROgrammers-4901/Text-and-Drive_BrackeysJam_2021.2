using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microgames;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<GameObject> commonGameObjects = new List<GameObject>();
    [SerializeField]
    private GameModes currentGameMode;

    [SerializeField] private float difficultyScaler = 30f;
    
    
    public float GameTime { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public float DrivingScore { get; set; }
    public float PhoneScore { get; set; }
    public GameObject PlayerObject { get; set; }
    public float PlayerSpeed { get; private set; }
    
    private bool paused;

    private void Awake()
    {
        PlayerSpeed = currentGameMode.playerStartSpeed;
        Difficulty = currentGameMode.initialDifficulty;

        StartGame();
    }

    private void Update()
    {
        if (!paused)
        { 
            GameTime += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        if(SceneManager.sceneCount < 3)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        }
    }

    public GameObject FindCommonGameObjectByName(string name) =>
        commonGameObjects.FirstOrDefault(commonGameObject => commonGameObject.name == name);

    public void AddGameObjectToCommon(GameObject gameObject) =>
        commonGameObjects.Add(gameObject);

    public MicrogameScriptableObject GetRandomMicrogame() =>
        currentGameMode.microgamesCollection.ElementAt(
        Random.Range(0, currentGameMode.microgamesCollection.Length)
        );

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        PauseGame();
        
        Debug.Log("GAME OVER");
        
        //TODO: Render GameOver Screen
    }
    
    public float GetMicrogameInterval() =>
        Random.Range(currentGameMode.minTimeBetweenMicrogames, currentGameMode.maxTimeBetweenMicrogames);
    
    public float GetScaledDifficulty() => (float) Difficulty + GameTime / difficultyScaler;
    public float GetScaledSpeed() => PlayerSpeed + GetScaledDifficulty() / 5;
}
