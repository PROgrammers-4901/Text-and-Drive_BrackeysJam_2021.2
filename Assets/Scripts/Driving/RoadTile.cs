using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : MonoBehaviour
{
    [SerializeField] private List<GameObject> Vehicles = new List<GameObject>();
    [SerializeField] private List<GameObject> Hazards = new List<GameObject>();
    
    public float TileDifficulty { get; private set; }

    void Awake()
    {
        foreach (var vehicle in Vehicles)
            vehicle.SetActive(false);
        foreach (var hazard in Hazards)
            hazard.SetActive(false);
        
        TileDifficulty = (float) GameManager.Instance.GetScaledDifficulty();

        if (Vehicles.Count + Hazards.Count < 2)
            TileDifficulty = 1;

        int obstaclesToSpawn = 2 + Mathf.RoundToInt(TileDifficulty);
        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            if (Random.Range(0, 2) == 0 && Vehicles.Count > 0)
                Vehicles[Random.Range(0, Vehicles.Count)].SetActive(true);
            else if (Hazards.Count > 0)
                Hazards[Random.Range(0, Hazards.Count)].SetActive(true);
        }
    }
}
