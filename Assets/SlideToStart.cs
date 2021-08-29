using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideToStart : MonoBehaviour
{
    [SerializeField] private float desiredValue = 1f;
    [SerializeField] private float threshold = 5f;

    [SerializeField] private List<GameObject> tutorials = new List<GameObject>();
    
    private Slider _slider;
    
    public void CheckIfValueIs(Single value)
    {
        if (Math.Abs(value - desiredValue) < threshold)
        {
            GameManager.Instance.StartGame();

            foreach (var go in tutorials)
            {
                Destroy(go);
            }
            
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (_slider.value > threshold)
            _slider.value -= Time.deltaTime*threshold;
    }
}
