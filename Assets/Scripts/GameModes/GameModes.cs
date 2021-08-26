using System.Collections;
using System.Collections.Generic;
using Microgames;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameMode", order = 1)]
public class GameModes : ScriptableObject
{
    public MicrogameScriptableObject[] microgamesCollection;

    [Header("Settings")]
    public bool allowRepetition = false;
    [Range(0f,10f)]
    public float minTimeBetweenMicrogames = 5f;
    [Range(0f,10f)]
    public float maxTimeBetweenMicrogames = 10f;

    public float playerStartSpeed = 1f;
}