using System.Collections;
using System.Collections.Generic;
using Microgames;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameModes : ScriptableObject
{
    public Microgame[] microgamesCollection;

    [Header("Settings")]
    public bool allowRepetition = false;
}