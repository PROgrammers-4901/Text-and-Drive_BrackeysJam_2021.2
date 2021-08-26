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
    private GameObject[] commonGameObjects;
    [SerializeField]
    private GameModes currentGameMode;
    
    [Header("Scoring")] 
    [SerializeField]
    private float _gameTime;

    public GameObject PlayerObject { get; private set; }
    public float PlayerSpeed { get; private set; }

    private void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerSpeed = currentGameMode.playerStartSpeed;
        
        if(!PlayerObject)
            throw new Exception("Could Not Find Player GameObject");
    }

    public GameObject FindCommonGameObjectByName(string name) =>
        commonGameObjects.FirstOrDefault(commonGameObject => commonGameObject.name == name);

    public void AddGameObjectToCommon(GameObject gameObject) =>
        commonGameObjects.Append(gameObject);

    public MicrogameScriptableObject GetRandomMicrogame() =>
        currentGameMode.microgamesCollection.ElementAt(
        Random.Range(0, currentGameMode.microgamesCollection.Length)
        );

    public float GetMicrogameInterval() =>
        Random.Range(currentGameMode.minTimeBetweenMicrogames, currentGameMode.maxTimeBetweenMicrogames);

    
}
