using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microgames;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<GameObject> commonGameObjects = new List<GameObject>();
    [SerializeField]
    private GameModes currentGameMode;
    
    public float GameTime { get; private set; }
    public Difficulty Difficulty { get; private set; }
    private bool paused;

    public float DrivingScore { get; set; }
    public float PhoneScore { get; set; }

    public GameObject PlayerObject { get; private set; }
    public float PlayerSpeed { get; private set; }

    private void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerSpeed = currentGameMode.playerStartSpeed;

        Difficulty = currentGameMode.initialDifficulty;
        
        if(!PlayerObject)
            throw new Exception("Could Not Find Player GameObject");
    }

    private void Update()
    {
        if (!paused)
        { 
            GameTime += Time.deltaTime;
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

    public float GetMicrogameInterval() =>
        Random.Range(currentGameMode.minTimeBetweenMicrogames, currentGameMode.maxTimeBetweenMicrogames);

    
}
