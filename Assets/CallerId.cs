using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CallerId : MonoBehaviour
{
    [SerializeField] private List<string> namePool = new List<string>();
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        
        _text.text = namePool[Random.Range(0, namePool.Count)] + " is calling...";
    }
}
