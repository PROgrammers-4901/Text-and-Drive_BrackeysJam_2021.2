using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Speedometer : MonoBehaviour
{
    void FixedUpdate()
    {
        this.transform.rotation = Quaternion.Lerp(
            this.transform.rotation, 
            Quaternion.Euler(new Vector3(
                0,
                0,
                -70 * (GameManager.Instance.GetScaledDifficulty() * Random.Range(.7f, 1.2f))
                ) 
            ), 
            .05f 
        );
    }
}
