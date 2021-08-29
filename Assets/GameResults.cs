using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameResults : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text timer;
    [SerializeField] private TMPro.TMP_Text counter;
    [SerializeField] private TMPro.TMP_Text score;
    
    
    private void OnEnable()
    {
        float gameTime = GameManager.Instance.GameTime;
        int phoneScore = (int)GameManager.Instance.PhoneScore;
        int gameScore = Mathf.CeilToInt(gameTime * phoneScore);
        
        int minutes = Mathf.FloorToInt( gameTime / 60);
        int seconds = (int)gameTime % 60;

        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        counter.text = phoneScore.ToString();

        score.text = gameScore.ToString();
    }
}
