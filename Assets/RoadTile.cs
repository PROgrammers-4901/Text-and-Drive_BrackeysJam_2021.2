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
        TileDifficulty = (float) GameManager.Instance.Difficulty + GameManager.Instance.GameTime / 30f;

        if (Vehicles.Count + Hazards.Count < 2)
            TileDifficulty = 1;
        
        
    }
}
