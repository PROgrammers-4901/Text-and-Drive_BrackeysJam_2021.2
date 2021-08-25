using System;
using TMPro;
using UnityEngine;

namespace Phone
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        private int seconds;
        private int minutes;
        

        private void Update()
        {
            float currentTime = Mathf.CeilToInt(Time.time - Time.deltaTime);
            seconds = Mathf.CeilToInt(currentTime) % 60;
            if (currentTime > 0 && seconds == 0)
                minutes++;

            
            
            timerText.text = (minutes*100 + seconds).ToString("00:00");
        }
    }
}
