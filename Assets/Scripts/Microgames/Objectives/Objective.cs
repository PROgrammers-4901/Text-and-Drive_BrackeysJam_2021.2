using System.Collections;
using System.Collections.Generic;
using Microgames;
using UnityEngine;

public class Objective : MonoBehaviour
{
    protected bool IsComplete { get; set; }

    protected float ElapsedTime { get; set; }

    private Microgame _parentMicrogame;

    protected void CompleteObjective()
    {
        IsComplete = true;
        _parentMicrogame.StartNextObjective();
    }
    public void SetParentMicrogame(Microgame parent) =>
        _parentMicrogame = parent;
}
