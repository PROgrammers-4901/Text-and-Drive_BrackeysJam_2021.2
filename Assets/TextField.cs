using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextField : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField inputField;
    
    [SerializeField] private string text;
    [SerializeField] private bool stepByWords;

    private string[] wordPool;
    private int index = 0;
    
    private void Start()
    {
        wordPool = text.Split(' ');
    }

    public void AddTextToField()
    {
        inputField.text += stepByWords ? wordPool[index] + " " : text[index].ToString();
        index = (index + 1) % (stepByWords ? wordPool.Length : text.Length);
    }
}