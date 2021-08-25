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

    private float idleTime;

    private void Update()
    {
        
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
