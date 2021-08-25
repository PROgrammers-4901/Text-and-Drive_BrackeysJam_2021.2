using System;
using UnityEngine;
using UnityEngine.UI;

namespace Microgames.Objectives
{
    public class SliderObjective : ObjectiveBase
    {
        [SerializeField] private float desiredValue = 100f;
    
        public void CheckIfValueIs(Single value)
        {
            if (Math.Abs(value - desiredValue) < 5f)
            {
                GetComponentInChildren<Image>().color = Color.green;

                if (!IsInvoking(nameof(IsCorrectValue)))
                    Invoke(nameof(IsCorrectValue), 1f);
            }
            else if (IsInvoking(nameof(IsCorrectValue)))
            {
                CancelInvoke(nameof(IsCorrectValue));
                CancelInvoke(nameof(CompleteObjective));
                IsInvalidValue();
            }
        }

        private void IsCorrectValue()
        {
            GetComponentInChildren<Image>().color = Color.blue;
            Invoke(nameof(CompleteObjective), .5f);
        }

        private void IsInvalidValue()
        {
            GetComponentInChildren<Image>().color = Color.red;
            Invoke(nameof(ReturnColor), .5f);
        }

        private void ReturnColor()
        {
            GetComponentInChildren<Image>().color = Color.white;
        }
    }
}
