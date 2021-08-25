using UnityEngine;

namespace Microgames.Objectives
{
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
}
