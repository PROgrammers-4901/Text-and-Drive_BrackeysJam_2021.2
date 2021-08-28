using System;
using TMPro;
using UnityEngine;

namespace Phone
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;

        private void Update()
        {
            float gameTime = GameManager.Instance.GameTime;
            int minutes = Mathf.FloorToInt( gameTime / 60);
            int seconds = (int)gameTime % 60;
            
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}
