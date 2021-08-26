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
    public GameObject GetPlayerObject { get; private set; }
    
    [Header("Scoring")] 
    [SerializeField]
    private float _gameTime;

    

    private void Awake()
    {
        GetPlayerObject = GameObject.FindGameObjectWithTag("Player");
        
        if(!GetPlayerObject)
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
