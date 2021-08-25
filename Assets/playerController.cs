using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    
    
    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.forward * moveSpeed;
    }
}
