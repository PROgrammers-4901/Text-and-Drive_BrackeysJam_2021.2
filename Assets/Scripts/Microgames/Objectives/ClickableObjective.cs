using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjective : ObjectiveBase
{
    [SerializeField] private int requiredClicks = 1;
    private int _clickCount = 0;
    
    public void IncrementClicks()
    {
        _clickCount++;
        
        if(_clickCount > requiredClicks)
            CompleteObjective();
    }
    
}
