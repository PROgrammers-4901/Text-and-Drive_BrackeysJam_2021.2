using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextField : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField inputField;
    
    [SerializeField] private List<string> textPool = new List<string>();
    [SerializeField] private bool stepByWords;

    private string[] wordPool;
    private int index = 0;
    private string text;
    
    private void Start()
    {
        text = textPool[Random.Range(0, textPool.Count)];
        
        wordPool = text.Split(' ');
    }

    public void AddTextToField()
    {
        inputField.text += stepByWords ? wordPool[index] + " " : text[index].ToString();
        index = (index + 1) % (stepByWords ? wordPool.Length : text.Length);
    }
}