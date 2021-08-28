using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResults : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text timer;
    [SerializeField] private TMPro.TMP_Text counter;
    
    private void OnEnable()
    {
        float gameTime = GameManager.Instance.GameTime;
        int minutes = Mathf.FloorToInt( gameTime / 60);
        int seconds = (int)gameTime % 60;

        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        counter.text = GameManager.Instance.PhoneScore.ToString();
    }
}
