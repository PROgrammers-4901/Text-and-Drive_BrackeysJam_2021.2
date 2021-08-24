using System;
using System.Collections;
using UnityEngine;

public class ClickMe : Objective
{
    [SerializeField] 
    private GameObject _clickableObject;

    void Update()
    {
        if (Input.GetButtonDown("Click"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hitInfo = Physics.RaycastAll(ray.origin, ray.direction);
            
            foreach (var hit in hitInfo)
            {
                if (hit.collider != null && hit.collider.gameObject == _clickableObject) 
                    CompleteObjective();
            }
        }
    }
}
