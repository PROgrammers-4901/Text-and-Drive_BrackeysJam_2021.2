using System;
using System.Collections;
using System.Collections.Generic;
using Microgames;
using UnityEngine;
using Random = System.Random;

public class MicrogameController : Singleton<MicrogameController>
{
    [SerializeField] private GameObject microgameContainer;
    private GameObject loadedMicroGame;
    private GameObject currentMicroGame;

    private float elapsedTime;
    
    private void Awake()
    {
        if(!microgameContainer)
            throw new Exception("Microgame Container Not Set");
    }

    private void Start()
    {
        FetchMicrogame();
        
        SpawnMicrogame();
    }

    void SpawnNotification()
    {
        // Spawn Sound
        
        // Load Phone Home Menu
        
        // Notification Slides in
        
        // Start Timer
    }
    
    void SpawnMicrogame()
    {
        if(loadedMicroGame)
        {
            currentMicroGame = Instantiate(loadedMicroGame, microgameContainer.transform);

            Microgame microgame = currentMicroGame.GetComponent<Microgame>();
            microgame.MicrogameCompleted.AddListener(HandleMicrogameComplete);
        }
        else
            FetchMicrogame();            
    }

    // Update is called once per frame
    void FetchMicrogame()
    {
        loadedMicroGame = GameManager.Instance.GetRandomMicrogame();
    }

    public void HandleMicrogameComplete(Microgame microgame)
    {
        Debug.Log("MICROGAME CONTROLLER EVENT HANDLER");
        
        Destroy(currentMicroGame);
        
        FetchMicrogame();
        
        Invoke(nameof(SpawnMicrogame), GameManager.Instance.GetMicrogameInterval());
    }
}
