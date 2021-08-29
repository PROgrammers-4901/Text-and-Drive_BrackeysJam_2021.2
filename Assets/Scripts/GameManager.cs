using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microgames;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<GameObject> commonGameObjects = new List<GameObject>();
    [SerializeField]
    private GameModes currentGameMode;

    [SerializeField] private float difficultyScaler = 30f;
    [SerializeField] private AudioMixer audioMixer;
    
    
    
    public float GameTime { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public float DrivingScore { get; set; }
    public float PhoneScore { get; set; }
    public GameObject PlayerObject { get; set; }
    public float PlayerSpeed { get; private set; }
    public bool GameStarted { get; private set; }
    
    private bool paused;
    private int scenesLoaded = 0;

    public float gameScore;

    private void Awake()
    {
        this.Reload();
        PlayerSpeed = currentGameMode.playerStartSpeed;
        Difficulty = currentGameMode.initialDifficulty;

        SceneManager.sceneLoaded += OnSceneLoaded;
        
        LoadScenes();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
            StartGame();
        
        if (PlayerObject == null)
            PlayerObject = GameObject.FindWithTag("Player");
        
        if (!paused && GameStarted)
        { 
            GameTime += Time.deltaTime;
        }
        
        
    }

    private void LoadScenes()
    {
        Time.timeScale = 1;
        
        if(SceneManager.sceneCount < 3)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        }
    }

    public void StartGame()
    {
        GameStarted = true;
        audioMixer.SetFloat("easyVolume", 0f);
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
        
        commonGameObjects.Find((go => go.name == "GameOverScreen")).SetActive(true);
    }
    
    public float GetMicrogameInterval() =>
        Random.Range(currentGameMode.minTimeBetweenMicrogames, currentGameMode.maxTimeBetweenMicrogames);
    
    public float GetScaledDifficulty() => (float) Difficulty + GameTime / difficultyScaler;
    public float GetScaledSpeed() => PlayerSpeed + GetScaledDifficulty() / 5;

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if(scene.buildIndex == 1 || scene.buildIndex == 2)
            scenesLoaded++;

        if (scenesLoaded >= 2 && SceneManager.GetSceneByBuildIndex(3).isLoaded)
        {
            SceneManager.UnloadSceneAsync(3);
            scenesLoaded = 0;
        }
            
    }
}
